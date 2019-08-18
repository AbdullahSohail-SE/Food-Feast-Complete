/*function DropdownToggler(button,item){
    let City=document.getElementById(button);
    let cities = document.querySelectorAll('#'+item);

    for(let city of cities){
        city.addEventListener('click', function(){
            City.textContent=city.textContent;
        });
    }
}

DropdownToggler('quantity-type', 'quantity-menu li');
DropdownToggler('payment-type', 'payment-menu li');*/

function DropdownToggler(button,field) {

    let City = document.querySelector('#' + button.getAttribute('id'));
    let cities = document.querySelectorAll('#' + button.getAttribute('id') + '+ul li');
            
	for (let city of cities) {
		city.addEventListener('click', function () {
            City.textContent = city.textContent;
            document.getElementById(field).value = city.textContent;
		});
	}
}

//let quantity = DropdownToggler('quantity-type');
//let payment = DropdownToggler('payment-type');

/*function DropdownToggler(button) {

	let cities = document.querySelectorAll('#' + button.id + "+ul li");

	for (let city of cities) {
		city.addEventListener('click',
			function() {
				button.textContent = city.textContent;
				return button.textContent;
			});
	}
}*/