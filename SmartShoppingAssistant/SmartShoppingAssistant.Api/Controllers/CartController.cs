using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartShoppingAssistant.BusinessLogic.DTOs.CartItem;
using SmartShoppingAssistant.BusinessLogic.Services.Interfaces;
using System.Security.Claims;

namespace SmartShoppingAssistant.Api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController(ICartService cartService) : ControllerBase
    {
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await cartService.GetCartAsync(UserId);
            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] CartItemCreateDTO dto)
        {
            try
            {
                var item = await cartService.AddItemAsync(dto, UserId);
                return CreatedAtAction(nameof(GetCart), item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("items/{itemId}")]
        public async Task<IActionResult> UpdateItem(int itemId, [FromBody] CartItemUpdateDTO dto)
        {
            try
            {
                var item = await cartService.UpdateItemQuantityAsync(itemId, dto, UserId);
                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            try
            {
                await cartService.RemoveItemAsync(itemId, UserId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            await cartService.ClearCartAsync(UserId);
            return NoContent();
        }

        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeCart()
        {
            try
            {
                var result = await cartService.AnalyzeCartAsync(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
