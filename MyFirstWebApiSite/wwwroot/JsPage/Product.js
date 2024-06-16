let categoryArr = [];

const addToBasket = (product) => {
    const productArr = JSON.parse(sessionStorage.getItem("Basket")) || []

    const index = productArr.findIndex(p => p.productId == product.productId)
    if (index != -1) {
        productArr[index].quantity += 1
    }
    else {
        product.quantity = 1;
        productArr.push(product);
    }
    sessionStorage.setItem("Basket", JSON.stringify(productArr))

    let sum = 0;

    productArr.forEach(p => { sum += p.quantity })
    document.getElementById('ItemsCountText').textContent = sum;


}

const clearBasket = () => {
    sessionStorage.removeItem("Basket");
}

const drawProducts = (data) => {
    
    const productArr = JSON.parse(sessionStorage.getItem("Basket")) || []
    let sum = 0;
    let count = 0;
    productArr.forEach(p => { sum += p.quantity })
    document.getElementById('ItemsCountText').textContent = sum;

    const template = document.getElementById("temp-card");

    data.forEach((product) => {
        count+=1
        const card = template.content.cloneNode(true)

        card.querySelector('.h1').textContent = product.productName
        card.querySelector('.price').textContent = product.price
        card.querySelector('.description').textContent = product.description
        card.querySelector('img').src = '../Image/' + product.imageUrl
        card.querySelector('button').addEventListener('click', () => addToBasket(product))
        document.getElementById("PoductList").appendChild(card)  
    })
    document.getElementById("counter").innerText = count

}


const getAllProducts = async () => {
    try {
        const responsePost = await fetch('api/Products', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!responsePost.ok) {
            throw new Error('Network response was not ok');
        }

        const dataPost = await responsePost.json();


        if (dataPost.length > 0) {
            drawProducts(dataPost);
            document.getElementById("minPrice").value = dataPost[0].price;
            document.getElementById("maxPrice").value = dataPost[dataPost.length - 1].price;
        } else {
            console.error('No products returned from the API');
        }
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
};


const filterProducts = async() => {

    const maxPrice = document.getElementById("maxPrice").value;
    const minPrice = document.getElementById("minPrice").value;
    const productName = document.getElementById("productName").value;
    let c = ''
    categoryArr.forEach(e => c += `&categories=${e}`)
    const responsePost = await fetch(`api/Products?minPrice=${minPrice}&maxPrice=${maxPrice}${c}&descrebtion=${productName}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const dataPost = await responsePost.json();
    console.log(dataPost)
    document.getElementById("PoductList").replaceChildren();
    drawProducts(dataPost);
    
}


const drawCategories = (data) => {

    const template = document.getElementById("temp-category");

    data.forEach(category => {
        const card = template.content.cloneNode(true)
        card.querySelector('.opt').id = category.categoryId
        card.querySelector('.opt').value = category.categoryName
        card.querySelector('label').for = category.categoryName
        card.querySelector('.OptionName').textContent = category.categoryName
        card.querySelector('.opt').addEventListener("change", (event) => {filterCategories(event, category)})

        document.getElementById("categoryList").appendChild(card)
    })


}

const getAllCategories = async () => {

    const responsePost = await fetch('api/Categories', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    const dataPost = await responsePost.json();

    drawCategories(dataPost);

}

const filterCategories = async (event, category) => {

    if (event.target.checked) {
        categoryArr.push(category.categoryId)
        filterProducts();
    }
    else {
        categoryArr.splice(categoryArr.indexOf(category.categoryId), 1)
        filterProducts();
    }
    
}

getAllProducts();
getAllCategories();