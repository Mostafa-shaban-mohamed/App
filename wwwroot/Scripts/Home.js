$(document).ready(function () {
    //on clicking login submit button
    $("#LoginBtn").click(function (e) {
        e.preventDefault();
        var email = $("#email").val();
        var password = $("#password").val();
        if (email && password) {
            let data = {
                email: email,
                password: password
            };
            $.ajax({
                type: 'POST',
                url: '/api/Admin/authenticate',
                data: JSON.stringify (data),
                success: function(data) {
                    if(data.token){
                        alert("You Have Been Authorized, Welcome Admin");
                        location.reload();
                    }
                    sessionStorage.setItem('JWT', JSON.stringify(data.token)); 
                },
                contentType: "application/json",
                dataType: 'json'
            });
        }
    });
});