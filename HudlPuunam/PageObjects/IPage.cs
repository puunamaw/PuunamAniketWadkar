namespace HudlPuunam.PageObjects
{
    public interface IPage
    {
        public void AssertTitle(string title);

        public void AssertH1Header(string expectedHeader);
    }
}
