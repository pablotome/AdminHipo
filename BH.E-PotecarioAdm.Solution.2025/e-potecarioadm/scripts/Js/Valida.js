function esDigito(sChr){
    var sCod = sChr.charCodeAt(0);
    return ((sCod > 47) && (sCod < 58));
   }
 
function valSep(oTxt){
var bOk = false;
var sep1 = oTxt.charAt(2);
var sep2 = oTxt.charAt(5);
bOk = bOk || ((sep1 == "-") && (sep2 == "-"));
bOk = bOk || ((sep1 == "/") && (sep2 == "/"));
return bOk;
}


function finMes(oTxt){
var nMes = parseInt(oTxt.substr(3, 2), 10);
var nAno = parseInt(oTxt.substr(6), 10);
var nRes = 0;
switch (nMes){
    case 1: nRes = 31; break;
    case 2: nRes = 28; break;
    case 3: nRes = 31; break;
    case 4: nRes = 30; break;
    case 5: nRes = 31; break;
    case 6: nRes = 30; break;
    case 7: nRes = 31; break;
    case 8: nRes = 31; break;
    case 9: nRes = 30; break;
    case 10: nRes = 31; break;
    case 11: nRes = 30; break;
    case 12: nRes = 31; break;
}
return nRes + (((nMes == 2) && (nAno % 4) == 0)? 1: 0);
}

function valDia(oTxt){
var bOk = false;
var nDia = parseInt(oTxt.substr(0, 2), 10);
bOk = bOk || ((nDia >= 1) && (nDia <= finMes(oTxt)));
return bOk;
}

function valMes(oTxt){
var bOk = false;
var nMes = parseInt(oTxt.substr(3, 2), 10);
bOk = bOk || ((nMes >= 1) && (nMes <= 12));
return bOk;
}

function valAno(oTxt){
var bOk = true;
var nAno = oTxt.substr(6);
bOk = bOk && ((nAno.length == 2) || (nAno.length == 4));
if (bOk){
    for (var i = 0; i < nAno.length; i++){
    bOk = bOk && esDigito(nAno.charAt(i));
    }
}
return bOk;
}

function valFecha(oTxt){
var bOk = true;
if (oTxt.value != ""){
    bOk = bOk && (valAno(oTxt));
    bOk = bOk && (valMes(oTxt));
    bOk = bOk && (valDia(oTxt));
    bOk = bOk && (valSep(oTxt));
    if (!bOk){
    return false;
    } else return true;
}
}

function ValidaFecha(value, element)
{
    //alert(value + " " + element);
    var res = true;
    if(!valFecha(value))
      { 
        res = false;
      }
    return res;
}
