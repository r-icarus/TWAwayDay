using CodePhile.Terminal;

namespace SpaceBeans {
    internal class YesNoMenu : Menu {
        public static bool Ask(string yesNoMessage) {
            var yesNo = new YesNoMenu();
            yesNo.Text = yesNoMessage;
            return yesNo.Choose<bool>();
        }

        public YesNoMenu() {
            Items.Add(new MenuItem { State = true, Text = "Yes" });
            Items.Add(new MenuItem { State = false, Text = "No" });
            Labels = new LabelSequence[] { new FixedLabelSequence("Y", "N") };
        }
    }
}