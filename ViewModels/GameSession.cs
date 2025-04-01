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

            CompleteQuestAtLocation();
            GivePlayerQuestAtLocation();
            GetMonsterAtLocation();
            CurrentTrader = CurrentLocation.TraderHere;
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

    private Trader? _currentTrader;

    public Trader? CurrentTrader
    {
        get => _currentTrader;
        private set
        {
            SetProperty(ref _currentTrader, value);
            OnPropertyChanged(nameof(HasTrader));
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

    public bool HasTrader => CurrentTrader != null;

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
        var newLocation = direction switch
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
        if (CurrentMonster?.CurrentHitPoints <= 0)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");

            CurrentPlayer.AddXp(CurrentMonster.RewardXp);
            RaiseMessage($"You receive {CurrentMonster.RewardXp} experience points.");
            CurrentPlayer.AddGold(CurrentMonster.Gold);
            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            foreach (var gameItem in CurrentMonster.Inventory)
            {
                CurrentPlayer.AddItem(gameItem);
                RaiseMessage($"You receive one {gameItem.Name}");
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
            if (CurrentPlayer.CurrentHitPoints <= 0)
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
            {
                CurrentPlayer.AddQuest(new QuestStatus(quest));
                RaiseMessage("");
                RaiseMessage($"You receive the '{quest.Name}' quest");
                RaiseMessage(quest.Description);

                RaiseMessage("Return with:");
                foreach (var itemQuantity in quest.ItemsToComplete)
                {
                    RaiseMessage(
                        $"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemId).Name}"
                    );
                }
                RaiseMessage("And you will receive:");
                RaiseMessage($"   {quest.RewardXP} experience points");
                RaiseMessage($"   {quest.RewardGold} gold");
                foreach (var itemQuantity in quest.RewardItems)
                {
                    RaiseMessage(
                        $"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemId).Name}"
                    );
                }
            }
        }
    }

    private void CompleteQuestAtLocation()
    {
        foreach (var quest in CurrentLocation.QuestsAvailableHere)
        {
            var questToComplete = CurrentPlayer.Quests.FirstOrDefault(q =>
                q.PlayerQuest.ID == quest.ID && !q.IsComplete
            );

            if (questToComplete != null)
            {
                if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                {
                    foreach (var itemQuantity in quest.ItemsToComplete)
                    {
                        for (var i = 0; i < itemQuantity.Quantity; i++)
                        {
                            CurrentPlayer.RemoveItem(
                                CurrentPlayer.Inventory.First(item =>
                                    item.ItemID == itemQuantity.ItemId
                                )
                            );
                        }
                    }
                    RaiseMessage("");
                    RaiseMessage($"You completed the '{quest.Name}' quest");
                    CurrentPlayer.AddXp(quest.RewardXP);
                    RaiseMessage($"You receive {quest.RewardXP} experiene points");
                    CurrentPlayer.AddGold(quest.RewardGold);
                    RaiseMessage($"You receive {quest.RewardGold} gold");
                    foreach (var itemQuantity in quest.RewardItems)
                    {
                        var rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemId);
                        CurrentPlayer.AddItem(rewardItem);
                        RaiseMessage($"You receive {rewardItem.Name}");
                    }
                    questToComplete.Complete();
                }
            }
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
