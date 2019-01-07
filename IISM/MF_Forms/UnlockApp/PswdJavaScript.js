
var button = document.getElementById("submit");
button.onclick = MyFunction;


function MyFunction()
{
    
    var value1 = document.getElementById("psw1").value;
    var value2 = "unlock-KE";

    if (value1 == value2)
    {
        alert('Password Match');
        RunExe();
        return true;
    }
    else
    {
        alert('NO EQUAL');
        return false;
    }


}
 
function RunExe()
{
    
    WshShell = new ActiveXObject("Wscript.Shell"); //Create WScript Object
    WshShell.run("c://IISM/IISM/IISM/bin/Release/IISM.exe");
    alert('Ejecuto la funcion');
   
}