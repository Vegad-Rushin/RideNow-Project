<%@ Page Title="" Language="C#" MasterPageFile="~/register.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="RideNow.register1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">

    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <title>RideNow - Sign up</title>
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">
        <link rel="stylesheet" href="css/bootstrap.css">
        <link rel="stylesheet" href="css/font-awesome.min.css">
        <link rel="stylesheet" href="css/main.css">
        <link rel="stylesheet" href="css/intro.css">
        <link rel="stylesheet" href="css/auth.css">
        <style>
            /* Style for client-side validation messages */
            .form-error {
                color: #dc3545;
                font-size: 0.875em;
                margin-top: 4px;
            }
            .form-control.error {
                border-color: #dc3545;
            }
            .form-control.success {
                border-color: #28a745;
            }

            .driverBtn {
                background-color:#de2626;
                color:white;
            }

            .driverBtn:hover {
                background-color:red;
            }

        </style>
    </head>
    <body>
        <header id="header">
            <div class="main-menu">
                <div class="header-content">
                    <a href="index.aspx" class="logo-text">RideNow</a>
                    <nav id="nav-menu-container">
                        <ul class="nav-menu">
                            <li><a href="index.aspx">Home</a></li>
                            <li><a href="about.aspx">About</a></li>
                            <li><a href="contact.aspx">Contact</a></li>
                            <li><a href="login.aspx" class="signup-btn">Log in</a></li>
                        </ul>
                    </nav>
                </div>
            </div>
        </header>
        <main class="auth-wrapper">
            <div class="auth-background">
                <div class="auth-overlay"></div>
            </div>
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-6 col-md-8">
                        <section class="auth-card scroll-animate-scale">
                            <div class="auth-header">
                                <h1 class="auth-title">Create your account</h1>
                                <p class="auth-subtitle">Join RideNow for fast, safe and affordable trips</p>
                            </div>

                            <div class="auth-form" id="registerForm">
                                <div class="form-row two-cols">
                                    <div class="form-group">
                                        <asp:Label runat="server" For="txtName">Full name</asp:Label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fa fa-user"></i></span>
                                            </div>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="John Doe" data-name="name" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" For="txtPhone">Phone</asp:Label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fa fa-phone"></i></span>
                                            </div>
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="+1 555 000 0000" TextMode="Phone" data-name="phone"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-row two-cols">
                                    <div class="form-group">
                                        <asp:Label runat="server" For="txtEmail">Email</asp:Label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fa fa-envelope"></i></span>
                                            </div>
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="you@example.com" TextMode="Email" data-name="email" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" For="txtPassword">Password</asp:Label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fa fa-lock"></i></span>
                                            </div>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Create password" TextMode="Password" data-name="password" required="required"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" For="txtConfirmPassword">Confirm Password</asp:Label>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fa fa-lock"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder="Confirm password" TextMode="Password" data-name="confirm-password" required="required"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row between">
                                    <div class="checkbox">
                                        <asp:CheckBox ID="chkAgree" runat="server" />
                                        <asp:Label runat="server" For="chkAgree" style="display: inline;">I agree to the <a href="#" class="link-inline">Terms</a> and <a href="#" class="link-inline">Privacy</a></asp:Label>
                                    </div>
                                </div>
                                <div id="chkAgreeError" class="form-error" style="display:none; text-align:left; margin-bottom: 15px;">You must agree to the terms and privacy policy.</div>
                                
                                <asp:Button ID="btnCreateAccount" runat="server" Text="Create account" OnClick="btnCreateAccount_Click" CssClass="btn btn-primary btn-block" />
                                <asp:Button ID="btnDriverAccount" runat="server" Text="Register as driver" OnClick="btnDriverAccount_Click" CssClass="btn btn-block driverBtn" FormNoValidate="true" />
                            </div>

                            <div class="auth-divider">
                                <span>or</span>
                            </div>
                            <div class="social-login">
                                <button class="btn btn-social btn-google">
                                    <i class="fa fa-google"></i>Continue with Google
                                </button>
                                <button class="btn btn-social btn-facebook">
                                    <i class="fa fa-facebook"></i>Continue with Facebook
                                </button>
                            </div>
                            <div class="auth-alt">
                                <span>Already have an account?</span> <a href="login.aspx" class="link-inline">Log in</a>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </main>

        <div class="coming-soon-modal" id="comingSoonModal">
            <div class="coming-soon-content">
                <div class="coming-soon-icon">
                    <i class="fa fa-rocket"></i>
                </div>
                <h3 class="coming-soon-title">Coming Soon!</h3>
                <p class="coming-soon-message">
                    This feature is currently under development. We're working hard to bring you an amazing experience. Stay tuned!</p>
                <button class="coming-soon-close" onclick="closeComingSoon()">
                    Got it!
                </button>
            </div>
        </div>

        <script src="js/vendor/jquery-2.2.4.min.js"></script>
        <script src="js/vendor/bootstrap.min.js"></script>
        
        <script>
            $(document).ready(function () {
                // Scroll Animation Observer
                const observerOptions = {
                    threshold: 0.1,
                    rootMargin: '0px 0px -50px 0px'
                };
                const observer = new IntersectionObserver(function (entries) {
                    entries.forEach(entry => {
                        if (entry.isIntersecting) {
                            entry.target.classList.add('animate');
                        }
                    });
                }, observerOptions);
                document.querySelectorAll('.scroll-animate, .scroll-animate-left, .scroll-animate-right, .scroll-animate-scale, .scroll-animate-rotate').forEach(el => {
                    observer.observe(el);
                });

                // Client-side validation before server-side postback
                $('#<%= btnCreateAccount.ClientID %>').on('click', function (e) {
                    if (!validateForm()) {
                        e.preventDefault(); // Stop the form submission if validation fails
                    }
                });

                // Real-time validation on blur
                $('.auth-form .form-control').on('blur', function () {
                    validateField($(this));
                });
            });

            function validateForm() {
                // --- Setup flags ---
                let areTextFieldsValid = true;
                let arePasswordsMatching = true;
                let isCheckboxChecked = true;

                // --- Clear Old Errors ---
                $('.form-error').remove();
                $('#chkAgreeError').hide();

                // --- 1. Validate Text Fields ---
                $('.auth-form .form-control[required]').each(function () {
                    // We use a temporary variable to not short-circuit the loop
                    let currentFieldValid = validateField($(this));
                    if (!currentFieldValid) {
                        areTextFieldsValid = false;
                    }
                });

                // --- 2. Validate Password Match ---
                const password = $('#<%= txtPassword.ClientID %>').val();
                const confirmPassword = $('#<%= txtConfirmPassword.ClientID %>').val();
                if (password !== confirmPassword && password.length > 0) { // Only check if password is not empty
                    const confirmField = $('#<%= txtConfirmPassword.ClientID %>');
                    confirmField.addClass('error').removeClass('success');
                    confirmField.closest('.form-group').find('.form-error').remove();
                    confirmField.closest('.input-group').after('<div class="form-error">Passwords do not match</div>');
                    arePasswordsMatching = false;
                }
                
                // --- 3. Validate Checkbox ---
                if (!$('#<%= chkAgree.ClientID %>').is(':checked')) {
                    $('#chkAgreeError').show();
                    isCheckboxChecked = false;
                }

                // --- 4. Display Appropriate Pop-up Alert ---
                const isFormFullyValid = areTextFieldsValid && arePasswordsMatching && isCheckboxChecked;

                if (!isFormFullyValid) {
                    // Check for the specific scenario user requested
                    if (areTextFieldsValid && arePasswordsMatching && !isCheckboxChecked) {
                        alert('You must agree to the Terms & Conditions to continue.');
                    } else {
                        // This is for any other combination of errors
                        alert('Please correct the errors and fill in all required fields to create an account.');
                    }
                }

                return isFormFullyValid;
            }

            function validateField(field) {
                const value = field.val().trim();
                const fieldName = field.data('name');
                let isValid = true;
                let errorMessage = '';

                // Remove previous error states for the current field
                field.removeClass('error success');
                field.closest('.form-group').find('.form-error').remove();

                if (field.prop('required') && value === '') {
                    isValid = false;
                    errorMessage = 'This field is required';
                }

                if (isValid && fieldName === 'email') {
                    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                    if (!emailRegex.test(value)) {
                        isValid = false;
                        errorMessage = 'Please enter a valid email address';
                    }
                }

                if (isValid && fieldName === 'password') {
                    if (value.length < 6) {
                        isValid = false;
                        errorMessage = 'Password must be at least 6 characters long';
                    }
                }

                if (isValid) {
                    field.addClass('success');
                } else {
                    field.addClass('error');
                    field.closest('.input-group').after(`<div class="form-error">${errorMessage}</div>`);
                }
                return isValid;
            }


            // Coming soon functionality for buttons
            $('.social-login button, .checkbox .link-inline').on('click', function (e) {
                e.preventDefault();
                showComingSoon();
            });

            // Coming Soon Modal Functions
            function showComingSoon() {
                $('#comingSoonModal').css('display', 'flex');
                $('body').css('overflow', 'hidden');
            }
            function closeComingSoon() {
                $('#comingSoonModal').css('display', 'none');
                $('body').css('overflow', 'auto');
            }
            $(document).on('click', '.coming-soon-modal', function (e) {
                if (e.target === this) {
                    closeComingSoon();
                }
            });
        </script>
    </body>
    </html>
</asp:Content>