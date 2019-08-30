using Microsoft.AspNetCore.Mvc;
using PayFI.NET.Library.Model.CheckoutFinland.Payment;
using PayFI.Web.Example.Provider;
using System;
using System.Collections.Generic;

namespace PayFI.Web.Example.Controllers
{

    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private readonly PaymentService paymentSvc;
        public PaymentController()
        {
            // TODO: No no no no no
            paymentSvc = new PaymentService();
        }

        [HttpPost("calculate-total")]
        public IActionResult CalculateTotal([FromBody]IEnumerable<Item> any)
        {
            try
            {
                return Ok(paymentSvc.GetPaymentProviders(any));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("success")]
        public IActionResult OnSuccessPayment()
        {
            return Ok();
        }

        [HttpGet("cancel")]
        public IActionResult OnCancelledPayment()
        {
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult MakePayment()
        {
            throw new NotImplementedException();
        }
    }
}
