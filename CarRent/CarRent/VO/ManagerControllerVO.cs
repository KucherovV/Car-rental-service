using System.Collections.Generic;
using CarRent.Common;
using System.Text;

namespace CarRent.VO
{
    public class ManagerControllerVO
    {
        public void OrderPageShown()
        {
            StaticLogger.LogTrace(this.GetType(), "Orders page succesfully shown");
        }

        public void OrdersDownloaded(List<int> orderIDs)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Orders have been successfully downloaded. IDs - ");
            foreach(var id in orderIDs)
            {
                sb.Append(id + ", ");
            }

            StaticLogger.LogDebug(this.GetType(), sb.ToString().Substring(0, sb.Length - 2));
        }

        public void OrderNotFound(string id)
        {
            StaticLogger.LogWarn(this.GetType(), "Order not found. ID - " + id);
        }

        public void WrongUrl(string id)
        {
            StaticLogger.LogWarn(this.GetType(), "Order not found. Wrong Url. ID - " + id);
        }

        public void OrderPassedForManaging()
        {
            StaticLogger.LogTrace(this.GetType(), "Order has been sent for managing");
        }

        public void ChangeStatus(string status)
        {
            StaticLogger.LogTrace(this.GetType(), "Order's status changed to " + status);
        }
    }
}