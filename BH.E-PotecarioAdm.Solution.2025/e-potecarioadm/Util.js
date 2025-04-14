
/*******SetControlesConCriterios ******************/
function controlCriterio_InputFocus(id)
{	
	var opcID = null;
	for (var i=0; i < arrayInput.length; i++)
	{				
		if (arrayInput[i] == id)		
		{
			opcID = arrayOpc[i]; break;
		}
	}			
	controlCriterio_OpcClick(opcID);	
}

function controlCriterio_OpcClick(opcID)
{
	var firstInput = null;

	for (var i=0; i < arrayOpc.length; i++)
	{				
		if (arrayOpc[i] == opcID)
		{
			getObj(arrayInput[i]).style.background = controlCriterioEnabled;
			
			if (firstInput == null)
				firstInput = arrayInput[i];			
		}
		else
		{
			getObj(arrayInput[i]).style.background = controlCriterioDisabled;				
		}			
	}		
	
	getObj(firstInput).focus();					
	getObj(firstInput).select();					
	getObj(opcID).checked = true;			
	
}

function forceSubmit_controlesCriterios(btnSubmitName)
{
	if (window.event.keyCode==13)
		getObj(btnSubmitName).click();
}
/***************************************************************/



function isDOM1()
{
	if (document.getElementById)
		return true;
	else
		return false;
}

function getObj(id)
{
	return document.getElementById(id);
}

function soloNumeros()
{
	if (event.keyCode < 48 || event.keyCode > 57)
		event.keyCode = 0;	
		
}

//****** SetClientRegExpValidation ***************/
function checkOnKeyPress(regEx)
{			
	var c = String.fromCharCode(event.keyCode);	
	if (! testRegExpValidation(regEx, c))		
		event.keyCode = 0;	
}

function checkOnPaste(regEx)
{
	var c = window.clipboardData.getData("Text");		
	return testRegExpValidation(regEx, c);		
}

function testRegExpValidation(regEx, c)
{
	var isValid = true;	
	var re = new RegExp(regEx);

	if (! re.test(c))
		isValid = false;
	else
	{	
		var sel = document.selection.createRange().text;
		var texto = event.srcElement.value.replace(sel, "");
		if (! re.test(texto + c))
			isValid = false;		
	}
	return isValid;
}

//****** Windows ***************/
function OpenCenterWin(url, name, width, height, scrollbar) 
{
	var left = Math.round(screen.availWidth / 2) - Math.round(width / 2);
	var top = Math.round(screen.availHeight / 2) - Math.round(height / 2); 
	OpenWin(url, name, width, height, left, top,  scrollbar);
}

function OpenWin(url, name, width, height, left, top,  scrollbar) 
{
	var strScroll = 'no';
	if (scrollbar) strScroll = 'yes'
	
	var size = ',width=' + width + ',height=' + height; 
 
	
	var posicion = ",left=" + left + ", top=" + top;
	
	var win = window.open(url, name, 'resizable=no,menubar=no,location=no,toolbar=no,status=no,scrollbars= ' + strScroll + ',directories=no,' + size + posicion);
	win.focus();
	
}
//***************************************************************/