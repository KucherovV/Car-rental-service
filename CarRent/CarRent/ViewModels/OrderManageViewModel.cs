using System.Collections.Generic;
using Entities;

namespace CarRent.ViewModels
{
    public class OrderManageViewModel
    {
        public ApplicationUser User { get; set; }

        public Order CurrentOrder { get; set; }

        public List<OrderProblemViewModel> OrderProblemViewModelsUserOrders { get; set; }

        public OrderConfirmDeny OrderConfirmDeny { get; set; }

        public List<AdditionalOption> AdditionalOptions { get; set; }

        public OrderProblem OrderProblem { get; set; }
    }
}