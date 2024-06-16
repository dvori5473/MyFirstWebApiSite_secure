
const getAllProducts = async () => {

    const products = JSON.parse(sessionStorage.getItem("Basket"));
    drawProducts(products)

}

const drawProducts = (products) => {

    const template = document.getElementById("temp-row");
    let tmp = 0;
    let count = 0;
    products.forEach(product => {
        tmp += product.quantity * product.price
        count+=product.quantity
        const card = template.content.cloneNode(true)

        card.querySelector('.itemName').textContent =product.productName
        card.querySelector('img').src = '../Image/' + product.imageUrl
        card.querySelector('.descriptionColumn').innerText = product.description
        card.querySelector('.price').innerText = product.price * product.quantity
        card.querySelector('.quantity').innerText = product.quantity
        card.querySelector(".DeleteButton").addEventListener('click', () => {product.quantity=1, removeFromBasket(product) });
        card.querySelector(".minus").addEventListener('click', () => { removeFromBasket(product) });
        card.querySelector(".plus").addEventListener('click', () => { addToBasket(product) });
        document.getElementById("itemList").appendChild(card)
    })
    document.getElementById("totalAmount").innerHTML = tmp

    document.getElementById("itemCount").innerHTML = count

}

addToBasket = (product) => {
    const products = JSON.parse(sessionStorage.getItem("Basket"))
    const index = products.findIndex(p => p.productId == product.productId);
    products[index].quantity += 1
    sessionStorage.setItem("Basket", JSON.stringify(products))
    document.getElementById("itemList").replaceChildren();
    drawProducts(products);
}

const removeFromBasket = (product) => {

    const products = JSON.parse(sessionStorage.getItem("Basket"))
    const index = products.findIndex(p => p.productId == product.productId);
    if (product.quantity==1) {
        products.splice(index, 1);
    }
    else {
        products[index].quantity-=1
    }
    sessionStorage.setItem("Basket",JSON.stringify(products))
    document.getElementById("itemList").replaceChildren();
    drawProducts(products);
}


const placeOrder = async() => {
    let orderItems = []
    const products = JSON.parse(sessionStorage.getItem("Basket"))

    if (!products) {
        alert("no products on basket")
        window.location.href = "Products.html"
    }

    products.forEach(p => orderItems.push({ "ProductId": p.productId, "ProductName": p.productName, "Quantitiy": p.quantity }))

    const OrderData = {
        "OrderDate": new Date(),
        "OrderSum": parseInt(document.getElementById('totalAmount').innerHTML),
        "UserId": JSON.parse(sessionStorage.getItem("user")).userId,
        "OrderItems": orderItems
    }
    
    const responseOrder = await fetch('api/Orders', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(OrderData)
    });
    if (responseOrder.ok) {
        sessionStorage.removeItem("Basket");
        alert("the order is succsess")
        window.location.href = "Products.html"
    }
    else {
        alert("error")
        window.location.href = "ShoppingBag.html"
    }
}

getAllProducts();