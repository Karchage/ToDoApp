"use strict";

var sign_in_btn = document.querySelector("#sign-in-btn");
var sign_up_btn = document.querySelector("#sign-up-btn");
var form_container = document.querySelector(".form-container");
sign_up_btn.addEventListener('click', function () {
  form_container.classList.add("sign-up-mode");
  sign_up_btn.classList.add("sign-up-mode");
  sign_in_btn.classList.add("sign-up-mode");
});
sign_in_btn.addEventListener('click', function () {
  form_container.classList.remove("sign-up-mode");
  sign_up_btn.classList.remove("sign-up-mode");
  sign_in_btn.classList.remove("sign-up-mode");
});