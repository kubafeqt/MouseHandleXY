
namespace MouseXY
{
    class Settings
    {
      //readonly:
      public static readonly Size defaultFormSize = new(462, 338); // default size of the form
      public static readonly Size biggerFormSize = new(870, 705);
      public static readonly Size panelSize = new(845, 650);
      public static readonly Point panelLocation = new(7, 7);

      //dynamic:
      public static Size latestSize = new();

      //mouse speed:
      public static int slowSpeed = 2;
      public static int normalSpeed = 10;
      public static int fastSpeed = 50;

      //from db settingsTable:
      public static int delayMs = 250;
      public static bool showDgvAfterSetKeyPos;

   }
}
