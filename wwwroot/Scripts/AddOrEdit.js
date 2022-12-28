$(document).ready(function () {
    //get Title (h3 element)
    if ($("#requestType").val() == "Add") {
        document.getElementById("Title").innerText = "Add New User";
        LoadingDDL();
    } else {
        document.getElementById("Title").innerText = "Edit User";
        LoadingDDL();
        EditMethod($("#elementID").val());
    }

    //enter Submit btn ====> here is where everything begins
    $("#formBtn").click(function (e) {
        e.preventDefault();
        formSubmittin();
    });

    function formSubmittin() {
        //get all values
        var userName = $("#Name").val();
        var userNatID = $("#National_ID").val();
        var userDoB = $("#DoB").val();
        var userAccount = $("#accountOptions").val();
        var userLoB = $("#loBOptions").val();
        var userLanguage = $("#languageOptions").val();
        var lvl = "";
        var markedCheckbox = document.getElementsByClassName('form-check-input');
        for (var checkbox of markedCheckbox) {
            if (checkbox.checked) {
                lvl += checkbox.value + ",";
            }
        };
        //check for validation to finally submit
        if (validation(userName, userNatID, userDoB, userAccount, userLoB, userLanguage, lvl)) {
            let data = {
                Name: userName,
                National_ID: userNatID,
                DoB: userDoB,
                Account: userAccount,
                LoB: userLoB,
                Language: userLanguage,
                Level: lvl
            };
            if ($("#requestType").val() == "Add") { //create new user
                //send post request to user controller with json body of values
                fetch("/api/User", {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json',
                        "Authorization": "Bearer " + JSON.parse(sessionStorage.getItem('JWT'))
                    },
                    body: JSON.stringify(data)
                }).then((response) => {
                    if(response.status != 200){
                        alert("You are unauthorized user, please login first");
                    }
                    $("#pages").load("UsersPage.html");
                });
            } else { //edit existing user
                var id = $("#elementID").val(); //get id of edited user
                //send put request to user controller with json body of values
                fetch("/api/User/"+id, {
                    method: "PUT",
                    headers: { 'Content-Type': 'application/json',
                        "Authorization": "Bearer " + JSON.parse(sessionStorage.getItem('JWT'))
                    },
                    body: JSON.stringify(data)
                }).then((response) => {
                    if(response.status != 200){
                        alert("You are unauthorized user, please login first");
                    }
                    $("#pages").load("UsersPage.html");
                });
            }

        } else {
            alert("Please check the fields again");
        }
    }

    //validation method
    //first check validations
    //if all values are in correct form then submit
    function validation(userName, userNatID, userDoB, userAccount, userLoB, userLanguage, lvl) {
        var isValid = true; //validation variable
        //check for validations
        if (!userName) { //name
            document.getElementById("nameValidation").style.display = "block";
            isValid = false;
        } else {
            document.getElementById("nameValidation").style.display = "none";
        }
        if (!userNatID || !userNatID.match(/^-?\d+$/) || userNatID.length != 14) { //national id
            document.getElementById("natIDValidation").style.display = "block";
            isValid = false;
        } else {
            document.getElementById("natIDValidation").style.display = "none";
        }
        var date = new Date(userDoB);
        var today = new Date();
        if (!userDoB || (today.getFullYear() - date.getFullYear()) < 18) { //date of birth
            document.getElementById("doBValidation").style.display = "block";
            isValid = false;
        } else {
            document.getElementById("doBValidation").style.display = "none";
        }
        if (userAccount == "Please Choose an Account") { //account
            document.getElementById("accountValidation").style.display = "block";
            isValid = false;
        } else {
            document.getElementById("accountValidation").style.display = "none";
        }
        if (userLoB == "Please Choose a Line of Business") { //line of business
            document.getElementById("loBValidation").style.display = "block";
            isValid = false;
        } else {
            document.getElementById("loBValidation").style.display = "none";
        }
        if (userLanguage == "Please Choose Language") { //language
            document.getElementById("languageValidation").style.display = "block";
            isValid = false;
        } else {
            document.getElementById("languageValidation").style.display = "none";
        }
        if (!lvl) { //level
            document.getElementById("lvlValidation").style.display = "block";
            isValid = false;
        } else {
            document.getElementById("lvlValidation").style.display = "none";
        }
        return isValid;
    }

    // Dropdownlist handler methods
    function LoBHandling() {
        //on change of account dropdownlist, line of business dropdownlist will change too.
        $("#accountOptions").change(function (e) {
            e.preventDefault();
            $("option").remove(".loB"); //reset options in (line of business) select element
            var acc = $("#accountOptions").val();
            $.ajax({
                url: "/api/Account/" + acc,
                type: 'GET',
                dataType: 'json', // added data type
                success: function (res) {
                    var i = 1; //record counter
                    res.forEach(element => {
                        $("#loBOptions").append(
                            `<option class="loB" value="${element.loB}">${element.loB}</option>`
                        );
                        i += 1;
                    });
                }
            });
        });
    }
    function AccountHandling() {
        //Account Dropdownlist
        $.ajax({
            url: "/api/Account",
            type: 'GET',
            dataType: 'json', // added data type
            success: function (res) {
                //get new array with non-duplicated account names            
                var dataArray = [];
                res.forEach(element => {
                    dataArray.push(element.account_Name);
                });
                let unique = [...new Set(dataArray)];
                //console.log(unique);
                unique.forEach(element => {
                    $("#accountOptions").append(
                        `<option class="accounts" value="${element}">${element}</option>`
                    );
                });
            }
        });
    }
    function LanguageHandling() {
        //language dropdownlist
        $.ajax({
            url: "/api/Language",
            type: 'GET',
            dataType: 'json', // added data type
            success: function (res) {
                var i = 1; //record counter
                res.forEach(element => {
                    $("#languageOptions").append(
                        `<option class="languages" value="${element.language_Name}">${element.language_Name}</option>`
                    );
                    i += 1;
                });
            }
        });
    }
    function LevelHandling() {
        //when language changes, the level checks will also change
        $("#languageOptions").change(function (e) {
            e.preventDefault();
            $("div").remove(".form-check");
            var lvl = $("#languageOptions").val();
            $.ajax({
                url: "/api/Language/" + lvl,
                type: 'GET',
                dataType: 'json', // added data type
                success: function (res) {
                    var myArray = res.level.split(",");
                    myArray.forEach(element => {
                        $("#languageLevelOptions").append(
                            `<div class="form-check">
                            <input class="form-check-input" type="checkbox" value="${element}" id="Levels">
                            <label class="form-check-label" for="level">
                                ${element}
                            </label>
                        </div>`
                        );
                    });
                }
            });
        });
    }

    //Loading DropDownLists
    function LoadingDDL() {

        AccountHandling(); //account handler

        LoBHandling(); //line of business handler

        LanguageHandling() //language handler

        LevelHandling() //language levels handler        
    }
    //put request handling
    function EditMethod(id) {
        $.ajax({
            url: "/api/User/" + id,
            type: 'GET',
            dataType: 'json', // added data type
            success: function (res) {
                //get all values
                $("#Name").val(res.name);
                $("#National_ID").val(res.national_ID);
                // convert date of birth to the correct format for input (type="date")
                var str = res.doB.substring(0, 10); //only get (yyyy-mm-dd) substring
                $("#DoB").val(str);
                //load account ddl
                $("#accountOptions").val(res.account);
                //load lob ddl, then add value to html element
                if ($("#accountOptions").val()) {
                    $.ajax({
                        url: "/api/Account/" + res.account,
                        type: 'GET',
                        dataType: 'json', // added data type
                        success: function (result) {
                            result.forEach(element => {
                                $("#loBOptions").append(
                                    `<option class="loB" value="${element.loB}">${element.loB}</option>`
                                );
                            });
                            $("#loBOptions").val(res.loB);
                        }
                    });
                }
                //load language ddl
                $("#languageOptions").val(res.language);
                //load checkboxes for langauge level
                $.ajax({
                    url: "/api/Language/" + res.language,
                    type: 'GET',
                    dataType: 'json', // added data type
                    success: function (result) {
                        var myArray = result.level.split(",");
                        myArray.forEach(element => {
                            $("#languageLevelOptions").append(
                                `<div class="form-check">
                                    <input class="form-check-input" type="checkbox" 
                                        value="${element}" id="Levels" ${res.level.includes(element) ? "checked" : ""}>
                                    <label class="form-check-label" for="level">
                                        ${element}
                                    </label>
                                </div>`
                            );
                        });
                    }
                });
            }
        });
    }
});