// temporary
//$(function(){
//	$(".home #banner").prepend('<a href="/CreateAPlate.aspx" style="position: absolute; top: 209px; left: 50%; margin-left: -172px;"><img src="/img/banner_big_overlay.png" /></a>');
//});

// defeat the background image flicker for image-replaced links in IE 6
if ( $.browser.msie && parseInt($.browser.version) <= 6 ) {
	try {
	 document.execCommand('BackgroundImageCache', false, true);
	} catch(e) {}
}

// new windows
$(function(){
	$("a[rel='external']").click(function() {
		window.open($(this).attr('href'));
		return false;
	});
});

/* SimpleModal (pop-ups)
******************************************************************/
$(function(){
	$(".pop").hide();
	$("a[rel='pop']").click(function (e) {
		e.preventDefault();
		$(this.hash).modal();
	});
});

/* accordion
******************************************************************/
$(function(){
	// initialize accordion
	$(".accordion > *")
		.filter(":even").addClass("accordion-topic").end()
		.filter(":odd").addClass("accordion-panel").hide();
	// click functionality
	$(".accordion > :even").click(function(){
		if (!$(this).hasClass("on")) {
			$(".accordion > :even.on").removeClass("on");
			$(this).addClass("on");
			$(".accordion > :odd:visible").slideUp("fast");
			$(this).next().slideDown("fast");
		}
	});
	// mouseover highlighting
	$(".accordion > :even").hover(function(){
		$(this).not(".on").addClass("hover");
	}, function(){
		$(this).removeClass("hover");
	});	
});

/* form validation
******************************************************************/
// functions

function trim(s) {
  return s.replace(/^\s+|\s+$/, '');
}
function validateZip(zip) {
	re = /^\d{5}(-\d{4})?$/;
	return re.test(zip);
}
function validatePhone(phone) {
	phone = phone.replace(/[\(\)\.\-\ ]/g, '');
	re = /^\d{10}$/;
	return re.test(phone);
}
function validateEmail(email) {
	re = /^.+@.+\..{2,6}$/;
	return re.test(email);
}
function validateCard(type, ccnum) {
	ccnum = ccnum.split(" ").join("");
	
	if (type == "VISA") {
		// Visa: length 16, prefix 4, dashes optional.
		var re = /^4\d{3}-?\d{4}-?\d{4}-?\d{4}$/;
	}
	else if (type == "MAST") {
		// Mastercard: length 16, prefix 51-55, dashes optional.
		var re = /^5[1-5]\d{2}-?\d{4}-?\d{4}-?\d{4}$/;
	}
	else if (type == "DISC") {
		// Discover: length 16, prefix 6011, dashes optional.
		var re = /^6011-?\d{4}-?\d{4}-?\d{4}$/;
	}
	else if (type == "AMEX") {
		// American Express: length 15, prefix 34 or 37.
		var re = /^3[4,7]\d{13}$/;
	}
	else if (type == "Diners") {
		// Diners: length 14, prefix 30, 36, or 38.
		var re = /^3[0,6,8]\d{12}$/;
	}
	if (!re.test(ccnum)) return false;
	// Remove all dashes for the checksum checks to eliminate negative numbers
	ccnum = ccnum.split("-").join("");
	
	// Checksum ("Mod 10")
	// Add even digits in even length strings or odd digits in odd length strings.
	var checksum = 0;
	for (var i=(2-(ccnum.length % 2)); i <= ccnum.length; i += 2) {
		checksum += parseInt(ccnum.charAt(i - 1));
	}
	// Analyze odd digits in even length strings or even digits in odd length strings.
	for (var i = (ccnum.length % 2) + 1; i < ccnum.length; i += 2) {
		var digit = parseInt(ccnum.charAt(i - 1)) * 2;
		if (digit < 10) checksum += digit;
		else checksum += (digit - 9);
	}
	if ((checksum % 10) == 0) return true;
	else return false;
}

// checkout form
$(function(){
	var containers = $('#checkout > div');
	var headers = $('#checkout > :header');

	// initialize
	$('<button type="button" class="next">Next</button>').appendTo(containers.filter(':not(:last)'));
	$('<div class="container edit"></div>').insertAfter(containers.filter(':not(:last)'));
	if(document.getElementById('order_confirm') != null && document.getElementById('order_confirm').checked == false) {
	    $('#checkout :input[name="PlaceOrder"]').addClass('disabled');
	    document.getElementById('PlaceOrder').disabled = true;
	} else {
	    if(document.getElementById('order_confirm') != null) {
	        $('#checkout :input[name="PlaceOrder"]').removeClass('disabled');
	        document.getElementById('PlaceOrder').disabled = false;
	    }
	}
	$('#checkout > div.edit').hide();
	containers.hide().filter(':first').show();
	headers.filter(':first').addClass('active');
	
	// next button click
	$('#checkout button.next').click(function () {
		var currentContainer = $(this).parent();
		var currentHeader = $(this).parent().prev();
		var currentFields = currentContainer.find(':input');
		var error = '';
		// remove any previous error indicators
		$('ul.error').remove();
		currentContainer.find('.error').removeClass('error');
		// validate form
		currentFields.each(function(){
			var value = trim($(this).val());
			//var labelElement = $('label[for='+$(this).attr('id')+']');
			var labelElement = $(this).prevAll('label');
			// if there is a 'title' attribute, use that for the error label instead of the 'label'
			if ($(this).is('*[title]')) {
				var label = $(this).attr('title');
			}
			else {
				var label = labelElement.text().replace(/\*/,'');
			}
			// required fields
			if ($(this).hasClass('required') && value == '') {
				error += '<li>'+label+' is a required field</li>';
				labelElement.addClass('error');
			}
			// zip
			if ($(this).is('#zip') && value != '') {
				if (!validateZip(value)) {
					error += '<li>'+label+' is not a valid format</li>';
					labelElement.addClass('error');
				}
			}
			// phone
			if ($(this).is('#phone') && value != '') {
				if (!validatePhone(value)) {
					error += '<li>'+label+' must be a valid number with area code</li>';
					labelElement.addClass('error');
				}
			}
			// email
			if ($(this).is('#email') && value != '') {
				if (!validateEmail(value)) {
					error += '<li>'+label+' is not a valid format</li>';
					labelElement.addClass('error');
				}
			}
			// credit card
			if ($(this).is('#card_num') && $('#card_type').val() != '' && value != '') {
				var cardTypeValue = $('#card_type').val();
				var cardTypeText = $('#card_type option[value='+cardTypeValue+']').text();
				if (!validateCard(cardTypeValue, value)) {
					error += '<li>'+label+' is not a valid '+cardTypeText+' card number</li>';
					labelElement.addClass('error');
				}
			}
		});
		// display error message or advance to next form section
		if (error != '') {
			//currentContainer.prepend('<ul class="error">'+error+'</ul>');
			$('<ul class="error">'+error+'</ul>').hide().prependTo(currentContainer).fadeIn();
		}
		else {
			containers.slideUp('slow');
			// display collected data for first checkout container
			if (currentHeader.is('#checkout-1')) {
				var name = currentFields.filter('#first_name').val()+' '+currentFields.filter('#last_name').val();
				if (currentFields.filter('#address_2').val() != '') {
					var address2 = currentFields.filter('#address_2').val();
				}
				var address = currentFields.filter('#address_1').val()+(address2?', '+address2:'');
				var cityStateZip = currentFields.filter('#city').val()+', '+currentFields.filter('#state').val()+' '+currentFields.filter('#zip').val();
				if (currentFields.filter('#phone').val() != '') {
					var phone = currentFields.filter('#phone').val();
				}
				if (currentFields.filter('#email').val() != '') {
					var email = currentFields.filter('#email').val();
				}
				var processedInfo = '<p><strong>'+name+'</strong><br />'+address+'<br />'+cityStateZip+(phone?'<br />'+phone:'')+(email?'<br />'+email:'')+'</p>';
				currentContainer.next().html(processedInfo);
			}
			// display collected data for second checkout container
			else {
				var cardTypeValue = currentFields.filter('#card_type').val();
				var cardTypeText = currentFields.filter('#card_type').children('option[value='+cardTypeValue+']').text();
				var cardNum = currentFields.filter('#card_num').val().split(' ').join('');
				var cardNumLast4 = cardNum.match(/\d{4}$/);
				currentContainer.next().html('<p><strong>'+currentFields.filter('#card_name').val()+'</strong><br />'+cardTypeText+' XXXX-XXXX-XXXX-'+cardNumLast4[0]+'<br />Expiration: '+currentFields.filter('#card_exp_month').val()+'/'+currentFields.filter('#card_exp_year').val()+'<br />Security Code:'+currentFields.filter('#card_code').val()+'</p>');
			}
			currentContainer.next().slideDown('slow');
			currentHeader.removeClass('active').addClass('edit');	
			if (currentHeader.nextAll().hasClass('previous')) {
				var nextHeader = currentHeader.nextAll('.previous');
				var nextContainer = nextHeader.next();
			}
			else {
				var nextContainer = currentContainer.nextAll('div:eq(1)');
				var nextHeader = currentHeader.nextAll(':header:eq(0)');
			}
			nextContainer.slideDown('slow');
			nextHeader.addClass('active');		
		}
		// clear error
		error = '';
		return false;
	});

	// edit button click
	headers.click(function () {
		var currentHeader = $(this);
		if (currentHeader.hasClass('edit')) {
			containers.slideUp('slow');
			// also slide up the current edit container
			currentHeader.nextAll('div:eq(1)').slideUp('slow');
			currentHeader.next().slideDown('slow');
			currentHeader.removeClass('edit').addClass('editing').addClass('active')
				.siblings('.active').removeClass('active').addClass('previous').end()
				.siblings('.editing').removeClass('active').addClass('edit')
				.nextAll('div.edit:eq(0)').slideDown('slow');
			//
			// need to add if statement to check for abandoned edit headers and containers
			//
		}
	});

	// agree to terms checkbox click
	$('input[id="order_confirm"]').click(function(){
		$('#checkout :input[name="PlaceOrder"]').toggleClass('disabled');
		document.getElementById('PlaceOrder').disabled = !document.getElementById('PlaceOrder').disabled;
	});	
	
	// submit button click
	$('#checkout :input[name="PlaceOrder"]').click(function () {
		var currentContainer = $(this).parent().parent();
		var confirmCheckbox = currentContainer.find(':checkbox');
		// remove any previous error indicators
		$('ul.error').remove();
		currentContainer.find('.error').removeClass('error');
		// if 'agree to terms' not checked, display error message 
		if (!confirmCheckbox.is(':checked')) {
			currentContainer.prepend('<ul class="error"><li>Please confirm that you agree to the terms and conditions</li></ul>');
			confirmCheckbox.addClass('error');
			return false;
		}
	});
	

// previous address click
 $('p.prev_address')
  .prepend('<input type="checkbox" />')
  .wrapInner('<label></label>')
  .find(':checkbox').click(function () {
   if ($(this).hasClass('checked')) {
    $(this).removeClass('checked')
     .parents('div.container').find(':input:not(:checkbox)')
      .val("")
      .removeAttr("disabled");
   }
   else {
    // 'click' any previously checked entries to 'undo' them
    $('input.checked').click();
    // add 'checked' class
    $(this).addClass('checked');
    var previousAddress = $(this).parent().html();
    var firstName = previousAddress.match(/<NAMEFIRST.*?>(.*?)</i);
    var lastName = previousAddress.match(/<NAMELAST.*?>(.*?)</i);
    var streetLine1 = previousAddress.match(/<STREET1.*?>(.*?)</i);
    var streetLine2 = previousAddress.match(/<STREET2.*?>(.*?)</i);
    var city = previousAddress.match(/<CITY.*?>(.*?)</i);
    var state = previousAddress.match(/<STATE.*?>(.*?)</i);
    var zip = previousAddress.match(/<ZIP.*?>(.*?)</i);
    var phone = previousAddress.match(/<PHONE.*?>(.*?)</i);
    var email = previousAddress.match(/<EMAIL.*?>(.*?)</i);
    $(':text#first_name').val(firstName[1]).attr("disabled", "disabled");
    $(':text#last_name').val(lastName[1]).attr("disabled", "disabled");
    $(':text#address_1').val(streetLine1[1]).attr("disabled", "disabled");
    $(':text#address_2').val((!streetLine2)?'':streetLine2[1]).attr("disabled", "disabled");
    $(':text#city').val(city[1]).attr("disabled", "disabled");
    $(':input#state').val(state[1]).attr("disabled", "disabled");
    $(':text#zip').val(zip[1]).attr("disabled", "disabled");
    $(':text#phone').val(phone[1]).attr("disabled", "disabled");
    $(':text#email').val(email[1]).attr("disabled", "disabled");
     // if (!$(':text#card_name').val()) {
      // $(':text#card_name').val(firstName[1]+' '+lastName[1]).attr("disabled", "disabled");
     // }
   }
  }); 

	
});

// contact us form
$(function(){
	// submit button click
	$('.contact :input[name="PlaceOrder"]').click(function () {
		var currentContainer = $(this).parent();
		var currentFields = currentContainer.find(':input');
		var error = '';
		// remove any previous error indicators
		$('ul.error').remove();
		currentContainer.find('.error').removeClass('error');
		// validate form
		currentFields.each(function(){
			var value = trim($(this).val());
			//var labelElement = $('label[for='+$(this).attr('id')+']');
			var labelElement = $(this).prevAll('label');
			// if there is a 'title' attribute, use that for the error label instead of the 'label'
			if ($(this).is('*[title]')) {
				var label = $(this).attr('title');
			}
			else {
				var label = labelElement.text().replace(/\*/,'');
			}
			// required fields
			if ($(this).hasClass('required') && value == '') {
				error += '<li>'+label+' is a required field</li>';
				labelElement.addClass('error');
			}
			// email
			if ($(this).is('#email') && value != '') {
				if (!validateEmail(value)) {
					error += '<li>'+label+' is not a valid format</li>';
					labelElement.addClass('error');
				}
			}
		});
		// display error message or advance to next form section
		if (error != '') {
			$('<ul class="error">'+error+'</ul>').hide().prependTo(currentContainer).fadeIn();
			return false
		}
	});

}); // end contact us form

/* faq page
******************************************************************/
$(function(){
	$(".faq dl > *").css("margin-left", "22px");
	// add numbers to each question
	$(".faq dt").css("position", "relative").each(function(i){
		i++;
		$(this).prepend('<span style="position:absolute; left:-22px;">'+i+'.</span>');
	});

});