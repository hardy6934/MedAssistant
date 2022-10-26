//function CheckIsUserAuth() {

//    const checkIsUserAuthUrl = `${window.location.origin}/accounts/isLoggedIn`;

//    fetch(checkIsUserAuthUrl)
//        .then(function (response) {
//            return response.json();
//        })
//        .then(function (result) {
//           return result;
//        }) 
//        .catch(function () {
//            console.error("something went wrong!");
//        });
//}

//let IsUserLoggedIn = CheckIsUserAuth();

//if (IsUserLoggedIn) {

//}
//else {

//    let navbar = document.getElementById('login-nav');

//}

let navbar = document.getElementById('login-nav');
const GetLoginPreviewUrl = `${window.location.origin}/accounts/UserLoginPreview`;

fetch(GetLoginPreviewUrl).then(function (response) {
    return response.text();
    })
    .then(function (response) {
    navbar.innerHTML = response;
    })
    .catch(function ( ) {
        console.log("something went wrong")
    });