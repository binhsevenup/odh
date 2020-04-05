$(document).ready(function () {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("form[name='registration']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side

            fullname: {
                required: true,
                rangelength: [3, 100]
            },
            username: {
                required: true,
                email: true
                // Specify that email should be validated
                // by the built-in "email" rule
              
            },
            password: {
                required: true,
                rangelength: [6, 100]
            },
            password_confirm: {
                required: true,
                rangelength: [6, 100],
                equalTo: '[name="password"]'
            },
            phone: {
                required: true,
                rangelength: [9, 12],
                number: true
            }
        },

        // Specify validation error messages
        messages: {

            fullname: {
                required: "* Please enter the full name.",
                rangelength: "* Please enter a value between 3 and 100 characters long."
            },
            password: {
                required: "* Please provide a password",
                rangelength: "* Please enter a value between 6 and 100 characters long."
               
            },
            password_confirm: {
                required: "* Please provide a confirm password",
                rangelength: "* Please enter a value between 6 and 100 characters long.",
                equalTo: "* Password does not match."
            },
           
            phone: {
                required: "* Please provide a phone",
                rangelength: "* Please enter a value between 9 and 12 number long.",
                number: "* Please enter the correct number format"

            },
            username: {
                required: "* Please enter a valid email address.",
                email: "* Please enter the correct email format"
              
            } 
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });
    $.validator.addMethod("valueNotEquals", function (value, element, arg) {
        return arg !== value;
    }, "Value must not equal arg.");

    $("form[name='RequestForm']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            facility: { valueNotEquals: "default" },
            category: { valueNotEquals: "default" },

            remarks: {
                required: true,
                rangelength: [5, 500]
            }

        },
        // Specify validation error messages
        messages: {

            remarks: {
                required: "* Please enter the messages.",
                rangelength: "* Please enter a value between 5 and 500 characters long."
            },
            facility: { valueNotEquals: "* Please select an item." },
            category: { valueNotEquals: "* Please select an item." }

        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            form.submit();
        }
    });

    $("form[name='LoginForm']").validate({

        rules: {

            username: {
                required: true,
                email: true
            },

            password: {
                required: true

            }

        },

        messages: {
            username: {
                required: "* Please enter email address.",
                email: "* Please enter the correct email format."
            },
            password: "* Please enter password."
        },
        submitHandler: function (form) {
            form.submit();
        }
    });

    $("form[name='RoleForm']").validate({
        rules: {
            name: {
                required: true,
                rangelength: [2, 50]

            }
        },

        messages: {
            name: {
                required: "* Please enter the role.",
                rangelength: "* Please enter a value between 2 and 50 characters long."
            }

        },
        submitHandler: function (form) {
            form.submit();
        }
    });

  
});



$(document).ready(function () {

    window.setTimeout(function () {
        $(".alert").fadeTo(1000, 0).slideUp(1000, function () {
            $(this).remove();
        });
    }, 3000);


});



//$(document).ready(function() {
//    $('#showAlert').click(function() {
//        $('#myAlert').simpleAlert({
//        message: "heloo alert"
//        });
//    });
//});

//$(document).ready(function () {
//    $("#btnSubmit").on("click", function () {
//        
//        if (confirm("Create Success")) {
//
//        } else {
//             location.reload();
//        }
//    });
//
//});

//    $(document).ready(function(){
//        $("#btnSubmit").on("click", function () {
//            alert('Your Message');
//        });
//  
//    });


//$(document).ready(function () {
//    $('[data-confirm]').click(function (e) {
//
//        if (!confirm($(this).attr("data-confirm"))) {
//            e.preventDefault();
//        } 
//        
//    });
//
//});

//$(document).ready(function() {
//    $('#btnSubmit').on("click", function() {
//
//        $.ajax({
//            type: 'POST',
//            url: '@Url.Action("Create", "FacilityCategories")',
//            dataType: 'json',
//            data: {
//                FacilityCategory_Id: FacilityCategory_Id,
//                FacilityCategory_Name: FacilityCategory_Name,
//                Created_At: Created_At,
//                Updated_At: Updated_At,
//                Status: Status
//            },
//            success: function (data) {
//
//                alert("Success");
//
//            },
//            error: function (a, b, c) {
//                alert("error!"); // 
//            }
//
//        });
//
//    });
//});