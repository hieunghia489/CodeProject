$(() =>{
    LoadFlowerData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/uploadList").build();
    connection.start();

    connection.on("LoadFlowers", function ()){
        LoadFlowerData();
    })
    LoadFlowerData();
    function LoadFlowerDate() {
        var tr = '';
        $.ajax({
            url: '/FlowerBouquetRepository/GetAll', method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                     tr += <tr>
                         <td>${v.FlowerBouquetName}</td>
                         <td>${v.Description}</td>
                         <td>${v.UnitPrice}</td>
                         <td>${v.UnitsInStock}</td>
                         <td>${v.FlowerBouquetStatus}</td>
                         <td>${v.Category.CategoryName}</td>
                         <td>${v.Supplier.SupplierName}</td>
                         <td>
                         <a href='../'></a>
                         </td>
                     </tr>
                   
                })
                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        });
    }
}
)
