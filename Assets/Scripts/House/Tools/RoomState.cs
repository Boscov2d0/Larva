namespace Larva.House.Tools
{
    public class HouseState
    {
        public enum RoomState
        {
            Null,
            OutSideMenu,
            MainHall,
            Kitchen,
            Bedroom,
            ChildrenRoom,
        }
        public enum ActionState 
        {
            Null,
            OpenWardrobe,
            CloseWardrobe,
            InstantiateParent,
            InstantiateChild
        }
    }
}