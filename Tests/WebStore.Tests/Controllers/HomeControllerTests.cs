using Microsoft.AspNetCore.Mvc;
using WebStore.Controllers;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers;

[TestClass]
public class HomeControllerTests
{
    [TestMethod]
    public void Contacts_returns_with_VIew()
    {
        // AAA = A-A-A = Arrenge - Act - Assert
        #region Arrange
        var controller = new HomeController(null!);
        #endregion

        #region Act
        var result = controller.Contacts();
        #endregion

        #region Assert
        var view_result = Assert.IsType<ViewResult>(result);

        //Assert.Equal(nameof(HomeController.Contacts), view_result.ViewName);//неправильное утверждение(для умпеха в HomeController Contacts добавить public IActionResult Contacts() => View(nameof(Contacts)))
        Assert.Null(view_result.ViewName);


        #endregion
    }
}
