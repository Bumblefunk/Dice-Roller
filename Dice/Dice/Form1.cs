namespace Dice
{
    public partial class Form : System.Windows.Forms.Form
    {
        RandomNumberService generateNumber = new RandomNumberService();
        ResultsList result = new ResultsList();

        public Form()
        {
            InitializeComponent();  
        }

        private void rollBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxUpper.Text))
            {
                label1.Text = "Empty";
                return;
            }
            bool canParse = int.TryParse(textBoxUpper.Text, out int dieFaces);
            if (!canParse)
            {
                label1.Text = "String";
                return;
            }
            if (dieFaces > 1)
            {
                ListDisplay.Clear();
                int returnedNumber = generateNumber.GenerateRandomNumberBetween(1, dieFaces);
                RollResult timedResult = new RollResult(returnedNumber);
                result.AddResult(timedResult.GetTimestampedResult());
                List<string> displayResult = result.GetResults();  
                for (int i = 0; i < displayResult.Count; i++)
                {
                    ListDisplay.AppendText(displayResult[i] + Environment.NewLine);
                }

            }
            else
            {
                label1.Text = dieFaces + " is too Low";
            }
        }
    }
}