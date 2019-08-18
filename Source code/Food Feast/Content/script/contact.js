function DropdownToggler(button,item){
    let City=document.getElementById(button);
    let cities = document.querySelectorAll('#'+item);

    for(let city of cities){
        city.addEventListener('click', function(){
            City.textContent = city.textContent;
            return City.textContent;
        });
    }
}
let type = DropdownToggler('select-type','type-menu li');
let city = DropdownToggler('select-city','city-menu li');