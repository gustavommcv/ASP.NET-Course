using Service_Contracts;
using Services;
using Services.DTO;
using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;

namespace ProjectTests
{
    public class OrderTests
    {
        private readonly IStocksService _stock;
        private readonly ITestOutputHelper _outputHelper;

        public OrderTests(ITestOutputHelper outputHelper) 
        {
            _stock = new StocksService();
            _outputHelper = outputHelper;
        }

        #region CreateBuyOrder
        // When you supply BuyOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public void CreateBuyOrder_IsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Arrange / Act
                var response = _stock.CreateBuyOrder(null);
            });
        }

        //When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1),
        //it should throw ArgumentException
        //When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000),
        //it should throw ArgumentException.
        //4. When you supply buyOrderPrice as 0 (as per the specification, minimum is 1),
        //it should throw ArgumentException.
        //When you supply buyOrderPrice as 10001 (as per the specification, maximum is 10000),
        //it should throw ArgumentException.
        //When you supply stock symbol=null (as per the specification, stock symbol can't be null),
        //it should throw ArgumentException.
        //When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification,
        //it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public void CreateBuyOrder_ModelValidation() 
        {
            // Arrange
            var trade = new BuyOrderRequest()
            {
                Price = 99999999,
                DateAndTimeOfOrder = new DateTime(1999 - 12 - 31),
                Quantity = 0
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(trade);

            bool isValid = Validator.TryValidateObject(trade, validationContext, validationResults, true);

            _outputHelper.WriteLine("Errors:");
            foreach (var validationResult in validationResults)
            {
                _outputHelper.WriteLine(validationResult.ErrorMessage);
            }

            // Assert
            Assert.Throws<ArgumentException>(() => 
            {
                var response = _stock.CreateBuyOrder(trade);
            });
        }

        //If you supply all valid values, it should be successful and return an object of
        //BuyOrderResponse type with auto-generated BuyOrderID (guid).
        [Fact]
        public async void CreateBuyOrder_ValidRequest()
        {
            var trade = new BuyOrderRequest()
            {
                StockName = "NameTest",
                StockSymbol = "MST",
                Price = 5,
                DateAndTimeOfOrder = new DateTime(2000, 01, 01),
                Quantity = 1
            };

            var response_from_create = await _stock.CreateBuyOrder(trade);

            var allOrders = await _stock.GetBuyOrders();

            // Contains
            Assert.True(response_from_create.BuyOrderID != Guid.Empty);
            Assert.Contains(response_from_create, allOrders);
        }
        #endregion

        #region GetAllBuyOrders
        // When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public async void GetAllBuyOrders_Empty()
        {
            var response = await _stock.GetBuyOrders();

            Assert.Empty(response);
        }

        // When you first add few buy orders using CreateBuyOrder() method;
        // and then invoke GetAllBuyOrders() method; the returned list should contain
        // all the same buy orders.
        [Fact]
        public async void GetAllBuyOrders_AddingOrders()
        {
            var trade1 = new BuyOrderRequest()
            {
                StockName = "BuyOrder1",
                StockSymbol = "MST",
                Price = 5,
                DateAndTimeOfOrder = new DateTime(2000, 01, 01),
                Quantity = 1
            };

            var trade2 = new BuyOrderRequest()
            {
                StockName = "BuyOrder2",
                StockSymbol = "MST",
                Price = 5,
                DateAndTimeOfOrder = new DateTime(2000, 01, 01),
                Quantity = 1
            };

            var response_from_create_1 = await _stock.CreateBuyOrder(trade1);
            var response_from_create_2 = await _stock.CreateBuyOrder(trade2);

            var allOrders = await _stock.GetBuyOrders();

            Assert.Contains(response_from_create_1, allOrders);
        }
        #endregion

        #region CreateSellOrder
        // When you supply SellOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public void CreateSellOrder_IsNull()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Arrange / Act
                var response = _stock.CreateSellOrder(null);
            });
        }

        //When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1),
        //it should throw ArgumentException
        //When you supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000),
        //it should throw ArgumentException.
        //4. When you supply sellOrderPrice as 0 (as per the specification, minimum is 1),
        //it should throw ArgumentException.
        //When you supply sellOrderPrice as 10001 (as per the specification, maximum is 10000),
        //it should throw ArgumentException.
        //When you supply stock symbol=null (as per the specification, stock symbol can't be null),
        //it should throw ArgumentException.
        //When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification,
        //it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public void CreateSellOrder_ModelValidation()
        {
            // Arrange
            var trade = new SellOrderRequest()
            {
                Price = 99999999,
                DateAndTimeOfOrder = new DateTime(1999 - 12 - 31),
                Quantity = 0
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(trade);

            bool isValid = Validator.TryValidateObject(trade, validationContext, validationResults, true);

            _outputHelper.WriteLine("Errors:");
            foreach (var validationResult in validationResults)
            {
                _outputHelper.WriteLine(validationResult.ErrorMessage);
            }

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                var response = _stock.CreateSellOrder(trade);
            });
        }

        //If you supply all valid values, it should be successful and return an object of
        //SellOrderResponse type with auto-generated SellOrderID (guid).
        [Fact]
        public async void CreateSellOrder_ValidRequest()
        {
            var trade = new SellOrderRequest()
            {
                StockName = "NameTest",
                StockSymbol = "MST",
                Price = 5,
                DateAndTimeOfOrder = new DateTime(2000, 01, 01),
                Quantity = 1
            };

            var response_from_create = await _stock.CreateSellOrder(trade);

            var allSellOrder = await _stock.GetSellOrders();

            Assert.Contains(response_from_create, allSellOrder);
        }
        #endregion

        #region GetAllSellOrders
        // When you invoke this method, by default, the returned list should be empty.
        [Fact]
        public async void GetAllSellOrders_Empty()
        {
            var response = await _stock.GetSellOrders();

            Assert.Empty(response);
        }

        [Fact]
        public async void GetAllSellOrders_AddingOrders()
        {
            var trade1 = new SellOrderRequest()
            {
                StockName = "BuyOrder1",
                StockSymbol = "MST",
                Price = 5,
                DateAndTimeOfOrder = new DateTime(2000, 01, 01),
                Quantity = 1
            };

            var trade2 = new SellOrderRequest()
            {
                StockName = "BuyOrder2",
                StockSymbol = "MST",
                Price = 5,
                DateAndTimeOfOrder = new DateTime(2000, 01, 01),
                Quantity = 1
            };

            var response_from_create_1 = await _stock.CreateSellOrder(trade1);
            var response_from_create_2 = await _stock.CreateSellOrder(trade2);

            var allOrders = await _stock.GetSellOrders();

            Assert.Contains(response_from_create_1, allOrders);
        }
        #endregion
    }
}
