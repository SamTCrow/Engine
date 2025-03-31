using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;

namespace Engine.ViewModels;

public class GameSession : ObservableObject
{
    public event EventHandler<GameMessagesEventArgs>? OnMessageRaised;

    public Player CurrentPlayer { get; }

    private Location _currentLocation;

    public Location CurrentLocation
    {
        get => _currentLocation;
        private set
        {
            SetProperty(ref _currentLocation, value);
            OnPropertyChanged(nameof(ExitNorth));
            OnPropertyChanged(nameof(ExitSouth));
            OnPropertyChanged(nameof(ExitWest));
            OnPropertyChanged(nameof(ExitEast));

            GivePlayerQuestAtLocation();
            GetMonsterAtLocation();
        }
    }

    public Weapon? CurrentWeapon { get; set; }
    private Monster? _currentMonster;

    public Monster? CurrentMonster
    {
        get => _currentMonster;
        private set
        {
            SetProperty(ref _currentMonster, value);
            OnPropertyChanged(nameof(HasMonster));

            if (CurrentMonster != null)
            {
                RaiseMessage("");
                RaiseMessage($"You see a {CurrentMonster.Name} here!");
            }
        }
    }

    public World CurrentWorld { get; }

    public bool HasMonster => CurrentMonster != null;

    public bool ExitNorth =>
        CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1)
        != null;

    public bool ExitSouth =>
        CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1)
        != null;

    public bool ExitWest =>
        CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate)
        != null;

    public bool ExitEast =>
        CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate)
        != null;

    public GameSession()
    {
        var name = "Sam";
        CurrentPlayer = new Player(name);
        CurrentPlayer.AddGold(1000000);
        CurrentWorld = WorldFactory.CreateWorld();
        _currentLocation = CurrentWorld.LocationAt(0, 0)!;
        if (CurrentPlayer.Weapons.Count == 0)
        {
            CurrentPlayer.AddItem(ItemFactory.CreateGameItem(1001));
        }
    }

    public void Move(Direction direction)
    {
        Location? newLocation = direction switch
        {
            Direction.North => CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate,
                CurrentLocation.YCoordinate + 1
            ),
            Direction.South => CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate,
                CurrentLocation.YCoordinate - 1
            ),
            Direction.East => CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate + 1,
                CurrentLocation.YCoordinate
            ),
            Direction.West => CurrentWorld.LocationAt(
                CurrentLocation.XCoordinate - 1,
                CurrentLocation.YCoordinate
            ),
            _ => null,
        };
        if (newLocation != null)
        {
            CurrentLocation = newLocation;
        }
    }

    public void AttackCurrentMonster()
    {
        if (CurrentWeapon == null)
        {
            RaiseMessage("You must select a weapon to attack.");
            return;
        }
        var damageToMonster = new Random().Next(
            CurrentWeapon.MinimumDamage,
            CurrentWeapon.MaximumDamage + 1
        );
        if (damageToMonster == 0)
        {
            RaiseMessage($"You missed the {CurrentMonster?.Name}");
        }
        else
        {
            CurrentMonster?.TakeDamage(damageToMonster);
            RaiseMessage($"You hit the {CurrentMonster?.Name} for {damageToMonster} points");
        }
        if (CurrentMonster?.HitPoints <= 0)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");

            CurrentPlayer.AddXp(CurrentMonster.RewardXp);
            RaiseMessage($"You receive {CurrentMonster.RewardXp} experience points.");
            CurrentPlayer.AddGold(CurrentMonster.RewardGold);
            RaiseMessage($"You receive {CurrentMonster.RewardGold} gold.");
            foreach (var itemQuantity in CurrentMonster.Inventory)
            {
                var item = ItemFactory.CreateGameItem(itemQuantity.ItemId);
                CurrentPlayer.AddItem(item);
                RaiseMessage($"You receive {itemQuantity.Quantity} {item.Name}");
            }
            GetMonsterAtLocation();
        }
        else
        {
            var damageToPlayer = new Random().Next(
                CurrentMonster!.MinimumDamage,
                CurrentMonster.MaximumDamage + 1
            );
            if (damageToPlayer == 0)
            {
                RaiseMessage("The monster attacks, but it misses you.");
            }
            else
            {
                CurrentPlayer.TakeDamage(damageToPlayer);
                RaiseMessage($"The {CurrentMonster.Name} hit you for {damageToPlayer} points.");
            }
            if (CurrentPlayer.Hitpoints == 0)
            {
                RaiseMessage("");
                RaiseMessage($"The {CurrentMonster.Name} killed you!");
                CurrentLocation = CurrentWorld.LocationAt(0, -1)!;
                CurrentPlayer.SetHp(CurrentPlayer.Level * 10);
            }
        }
    }

    private void GivePlayerQuestAtLocation()
    {
        foreach (var quest in CurrentLocation.QuestsAvailableHere)
        {
            if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                CurrentPlayer.AddQuest(new QuestStatus(quest));
        }
    }

    private void GetMonsterAtLocation()
    {
        CurrentMonster = CurrentLocation.GetMonster();
    }

    private void RaiseMessage(string message)
    {
        OnMessageRaised?.Invoke(this, new GameMessagesEventArgs(message));
    }
}

public enum Direction
{
    North,
    South,
    East,
    West,
}
