using System.Collections.Generic;
using CarRent.Common;
using System.Text;

namespace CarRent.VO
{
    public class UserControllerVO
    {
        public void CarIndexShown()
        {
            StaticLogger.LogTrace(this.GetType(), "Cars page succesfully shown");
        }

        public void CarListDownloadedShown(int[] carIDs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Car list successfully downloaded. ID's - ");
            foreach(var id in carIDs)
            {
                sb.Append(id + ", ");
            }

            StaticLogger.LogDebug(this.GetType(), sb.ToString().Substring(0, sb.Length - 2));
        }

        public void UserIsNotLogined()
        {
            StaticLogger.LogWarn(this.GetType(), "Form has not been sent. User is not logined");
        }

        public void UserHasDebt()
        {
            StaticLogger.LogWarn(this.GetType(), "Form has not been sent. User has debt");
        }

        public void WrongUrl(string id)
        {
            StaticLogger.LogWarn(this.GetType(), "Form has not been sent. Wrong Url. ID - " + id);
        }

        public void CarNotFound(string id)
        {
            StaticLogger.LogWarn(this.GetType(), "Car not found. ID - " + id);
        }

        public void FormSent()
        {
            StaticLogger.LogTrace(this.GetType(), "Form has been successfully sent");
        }

        public void FormDownloadedd()
        {
            StaticLogger.LogTrace(this.GetType(), "Form has successfully downloaded");
        }

        public void FormIsNotValid(IEnumerable<string> errors)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Car list successfully downloaded. ID's - ");
            foreach (var error in errors)
            {
                sb.Append(error + ", ");
            }

            StaticLogger.LogWarn(this.GetType(), sb.ToString().Substring(0, sb.Length - 2));
        }

        public void NoFreeStoks()
        {
            StaticLogger.LogWarn(this.GetType(), "No free stocks");
        }

        public void OrderSaved()
        {
            StaticLogger.LogInfo(this.GetType(), "Order has been successfully saved");
        }
    }
}