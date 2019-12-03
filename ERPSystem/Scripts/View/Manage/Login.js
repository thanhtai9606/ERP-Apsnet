
var HTMLLogin = (function () {
   
    var validLogin = function () {

        var currentUser = $('#form').serialize();       
        // ajax load process and valid Login
        $.ajax({
            url: "/Authentication/ValidateLogin/",
            type: "POST",
            data: currentUser,
            dataType: "JSON",
            success: function (data) {
                console.log(data.statusCode);
                if (data.statusCode === 200) //if success close modal and reload ajax table
                {

                    new PNotify({
                        title: "Thông báo!",
                        type: "success",
                        text: data.status,
                        nonblock: {
                            nonblock: false
                        }
                    });
                    window.setTimeout(function () {
                        window.location.href = '/Home/Index';
                    }, 1000);
                }
                else
                    if (data.statusCode === 400) {
                        new PNotify({
                            title: "Lỗi đăng nhập!",
                            type: "dager",
                            text: data.status,
                            nonblock: {
                                nonblock: false
                            }
                        })
                        //window.setTimeout(function () {
                        //    window.location.href = 'Login';
                        //}, 2000);
                    }

            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Error adding / update data');

            }
        });

    }
    return {
        resetForm: function () {
            $('#form2')[0].reset(); // reset form on modals
        },
        loginEvent : function () {
            document.addEventListener("keydown", function (event) {               
                if (event.keyCode === 13) {
                    validLogin();
                }
            });
        },
        onClick: function () {
            validLogin();
            
        },
       
    }
    
})();
HTMLLogin.loginEvent();

