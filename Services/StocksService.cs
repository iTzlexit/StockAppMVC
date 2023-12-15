using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StocksService : IStocksService
    {
        //Private fields for the in memory data 

        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;


        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>(); 

        }



        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            // Vallidation : buyOrderRequest cant be null 

            if(buyOrderRequest == null) 
                throw new ArgumentNullException(nameof(buyOrderRequest));

            //Model Validation 

            ValidationHelper.ModelValidation(buyOrderRequest); 

            //convert into by order Type 

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            // generate a new Buy Order Id 

            buyOrder.BuyOrderID = Guid.NewGuid();

            //Add buy order object to buy ordrs list 
            _buyOrders.Add(buyOrder);
            //convert  the BuyOrders object into the buy orders response 

            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            //Validation: sellOrderRequest can't be null
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            //Model validation
            ValidationHelper.ModelValidation(sellOrderRequest);

            //convert sellOrderRequest into SellOrder type
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            //generate SellOrderID
            sellOrder.SellOrderID = Guid.NewGuid();

            //add sell order object to sell orders list
            _sellOrders.Add(sellOrder);

            //convert the SellOrder object into SellOrderResponse type
            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            throw new NotImplementedException();
        }

        public List<SellOrderResponse> GetSellOrders()
        {
            throw new NotImplementedException();
        }
    }
}
