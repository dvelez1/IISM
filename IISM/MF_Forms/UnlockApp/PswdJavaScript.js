
var button = document.getElementById("submit");
button.onclick = MyFunction;

function MyFunction() {


    var value1 = document.getElementById("psw1").value;
    var value2 = "unlock-KE";

    if (value1 == value2)
    {
        alert('EQUAL');
        return true;
    }
    else
    {
        alert('NO EQUAL');
        return false;
    }


}