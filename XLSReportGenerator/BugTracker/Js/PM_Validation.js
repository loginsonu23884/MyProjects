


//   $("ContentPlaceHolder1_btnReset").click(functionalert);
////             alert("Hello") ;
////   });



function functionalert() {
    alert("Hello");
}


function fnNumericCheck(numberCheck) {
    var regexAssignmentNumber = /^(?!^0)\d{1,9}$/ ;
    if (regexAssignmentNumber.test(numberCheck)) {
        return true;
    } else {
        return false;
    }
}

function fnNumericDecimalCheck(rate) {
    var regexRate = /^[+-]?\d+(\.\d+)?$/ ;

    if (regexRate.test(rate)) {
        return true;
    } else {

        return false;
    }
}


function fnWebsideCheck(website) {
    var regUrl = /^(((ht|f){1}(tp:[/][/]){1})|((www.){1}))[-a-zA-Z0-9@:%_\+.~#?&//=]+$/ ;

    if (regUrl.test(website)) {
        return true;
    } else {
        return false;
    }
}

 //Assignment number check on key press.....

function funAssignmentTextCheck(e) {
    var charCode = e.which || e.keyCode;
    if (charCode > 3 && (charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 37) && (charCode != 39))
        return false;
    return true;
}


    //Begin date & Duration date......

function fnValidDate() {
    return false;
}

  //Project record by assignment number....

function GetRecordByAssignmentNumber(assignmentNumber) {
    var customer_id, customer_name, project_name;

    $.ajax({         
        type: "POST",

        url: "/SalesReporting/PM_Ajax.aspx",

        data: { status: "GetProjectRecordByAssignmentNumber", AssignmentNumber: assignmentNumber },

        dataType: "xml",

        success: function(xml) {
            $("select[name$=ddlClientName]>option").remove();

            $(xml).find('CustomerSelect').each(function() {

                project_name = $(this).find('project_name').text();

                alert(project_name);

                customer_id = $(this).find('customer_id').text();

                customer_name = $(this).find('customer_name').text();

                // $("select[name$=txtProjectName]").append(val(project_name).html(project_name));

                $("select[name$=ddlClientName]").append($("<option></option>").val(customer_id).html(customer_name));

            });
        },
        error: function(xml) {
            alert("not inside success");
            // alert(xml);                          
        }
    });

}