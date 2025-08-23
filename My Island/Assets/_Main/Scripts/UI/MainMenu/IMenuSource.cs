namespace MyIsland
{
    public interface IMenuSource
    {
        bool IsMenuOpen { get; }
        void OpenPage(MenuPage menuPage);
        void CloseMenu();
    }
}
