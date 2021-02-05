namespace Sp1sok_del
{
    class Delas 
    {
        private string title;
        private string delo;
        private string password;
        private string dateTime;
        public int id {get; set;}
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
            }
        }
        public string Delo
        {
            get { return delo; }
            set
            {
                delo = value;
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }
        public string DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
            }
        }
    }
}