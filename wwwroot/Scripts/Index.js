$(document).ready(function(){
    //when it starts
    //check if admin is registered
    if(sessionStorage.getItem('JWT') != ""){ //show log out btn
        document.getElementById("LogoutBtn").style.display = "block";
    }else{ //hide log out btn
        document.getElementById("LogoutBtn").style.display = "none";
    }
    
    loadPages();

    $("#LogoutBtn").click(function () {  
        sessionStorage.setItem('JWT', "");
        alert("You Have Loged out, Goodbye Admin");
        location.reload(); 
    })

    //when click the button in navbar become "active"
    $(".nav-item").click(function() {
        document.getElementsByClassName("active")[0].classList.remove("active");
        $(this).addClass("active");
        loadPages();
    });    
});

function loadPages(){ //check what button is active
    if(document.getElementById("home").classList.contains('active')){
        $("#pages").load("HomePage.html");
    }else if(document.getElementById("users").classList.contains('active')){
        $("#pages").load("UsersPage.html");
    }else if(document.getElementById("dashboard").classList.contains('active')){
        $("#pages").load("DashboardView.html");
    }
}