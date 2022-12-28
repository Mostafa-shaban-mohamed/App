$(document).ready(function () {
    $.ajax({
        url: "/api/User",
        type: 'GET',
        dataType: 'json', // added data type
        success: function (res) {
            //insert new row with record from json response
            var i = 1; //record counter
            res.forEach(element => {
                var date = new Date(element.doB); //get date of birth
                $("#rows").append(
                    `<tr id="${element.national_ID}">
                        <th scope="row">${i}</th>
                        <td>${element.name}</td>
                        <td>${element.national_ID}</td>
                        <td>${date.getFullYear().toString()}</td>
                        <td>${element.age}</td>
                        <td>${element.account}</td>
                        <td>${element.loB}</td>
                        <td>${element.language}</td>
                        <td>${element.level}</td>
                        <td>
                            <button id="${element.id}" type="button" class="btn btn-success btn-sm edit">Edit</button>
                            <button id="${element.id}" type="button" class="btn btn-danger btn-sm delete">Delete</button>
                        </td>
                    </tr>`
                );
                i += 1;
            });
            // delete a record
            $(".delete").click(function (e) {
                e.preventDefault();
                if (sessionStorage.getItem('JWT') != "") {
                    if (confirm("Are you sure to delete this record?")) {
                        fetch('/api/User/' + e.target.id, {
                            method: 'DELETE',
                            headers: {
                                'Content-Type': 'application/json',
                                "Authorization": "Bearer " + JSON.parse(sessionStorage.getItem('JWT'))
                            },
                        }).then(() => {
                            location.reload();
                        });
                    }
                } else {
                    alert("You are unauthorized User, Please Login first");
                }
            });
            // edit a record
            $(".edit").click(function (e) {
                e.preventDefault();
                if (sessionStorage.getItem('JWT') != "") {
                    $("#pages").load("AddOrEdit.html", function () {
                        $("#elementID").val(e.target.id);
                        $("#requestType").val("Edit");
                    });
                } else {
                    alert("You are unauthorized User, Please Login first");
                }

            });
        }
    });
    //on click of new user button, new page is loaded
    $("#addUserBtn").click(function (e) {
        e.preventDefault();
        if (sessionStorage.getItem('JWT') != "") {
            $("#pages").load("AddOrEdit.html", function () {
                $("#requestType").val("Add");
            });
        } else {
            alert("You are unauthorized User, Please Login first");
        }

    });
    //on click of call export method in user controller
    //to export user table from database in csv file format (for excel)
    $("#exportCSVfile").click(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/api/User/export",
            type: 'GET',
            success: function (res) {
                alert(res);
                var encodedUri = encodeURI(res);
                var hiddenElement = document.createElement('a');
                hiddenElement.href = 'data:text/csv;charset=utf-8,' + encodedUri;
                hiddenElement.target = '_blank';

                //provide the name for the CSV file to be downloaded  
                hiddenElement.download = 'Users.csv';
                hiddenElement.click();

                //var exportedFilename = 'export.csv';
                //var blob = new Blob([res], { type: 'text/csv;charset=utf-8;' });
                //var link = document.getElementById("exportCSVfile");
                //if (link.download !== undefined) { // feature detection
                // Browsers that support HTML5 download attribute
                //    var url = URL.createObjectURL(blob);
                //    link.setAttribute("href", "#");
                //    link.setAttribute("download", exportedFilename);
                //    link.click();
                //}
            }
        });
    });
    //when opening csv file, it might say that the file is corrupted, say yes anyway it will work
});
