$(document).ready(function () {
    // Hide all error messages initially for both forms
    $('#register-form .form-group span, #login-form .form-group span').hide();

    // Toggle between login and register forms
    $('.toggle-btn').click(function () {
        $('.toggle-btn').removeClass('active');
        $(this).addClass('active');
        var target = $(this).data('target');
        $('.form-container').hide();
        $(target).fadeIn(500);
    });

    // Function to validate phone number
    function validatePhoneNumber(phoneNumber) {
        const regex = /^07\d{8}$/; // 10 digits starting with 07
        return regex.test(phoneNumber);
    }

    // Function to validate password
    function validatePassword(password) {
        const regex = /^(?=.*[0-9])(?=.*[\W_]).{6,}$/; // At least one number, one special character, minimum 6 characters
        return regex.test(password);
    }

    // Register Form Submission
    $('#register-form form').submit(function (event) {
        event.preventDefault();
        $('#register-form .form-group span').hide(); // Hide previous messages

        const firstName = $('#register-first-name').val().trim();
        const lastName = $('#register-last-name').val().trim();
        const phoneNumber = $('#register-phone-numbe').val().trim();
        const email = $('#register-email').val().trim();
        const password = $('#register-password').val();
        const confirmPassword = $('#register-confirm-password').val();

        let valid = true;

        // Validate first name
        if (!firstName) {
            $('#register-first-name').next('span').text('First Name is required.').show();
            valid = false;
        }

        // Validate city
        const city = $('#register-city').val();
        if (!city) {
            $('#register-city').next('span').text('City is required.').show();
            valid = false;
        }

        // Validate last name
        if (!lastName) {
            $('#register-last-name').next('span').text('Last Name is required.').show();
            valid = false;
        }

        // Validate phone number
        if (!validatePhoneNumber(phoneNumber)) {
            $('#register-phone-numbe').next('span').text('Phone number must be 10 digits and start with "07".').show();
            valid = false;
        }

        // Validate email
        if (!email) {
            $('#register-email').next('span').text('Email is required.').show();
            valid = false;
        }

        // Validate password
        if (!validatePassword(password)) {
            $('#register-password').next('span').text('Password must be at least 6 characters long, with one number and one special character.').show();
            valid = false;
        }

        // Check if passwords match
        if (password !== confirmPassword) {
            $('#register-confirm-password').next('span').text('Passwords do not match.').show();
            valid = false;
        }

        // If valid, perform any desired action (like form submission)
        if (valid) {
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "You're in! Your account has been set up.",
                showConfirmButton: false,
                timer: 3000
            });
            // Your logic for successful registration can go here
        }
    });

    // Login Form Submission
    $('#login-form form').submit(function (event) {
        event.preventDefault();
        $('#login-form .form-group span').hide(); // Hide previous messages

        const email = $('#login-email').val().trim();
        const password = $('#login-password').val().trim();

        let valid = true;

        // Validate email
        if (!email) {
            $('#login-email').next('span').text('Email is required.').show();
            valid = false;
        }

        // Validate password
        if (!password) {
            $('#login-password').next('span').text('Password is required.').show();
            valid = false;
        }

        // If valid, perform any desired action (like form submission)
        if (valid) {
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "Welcome! Access granted to your account.",
                showConfirmButton: false,
                timer: 2000
            });
            // Your logic for successful login can go here
        }
    });
});
