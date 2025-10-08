<%@ Page Title="" Language="C#" MasterPageFile="~/driver/driverRegister.Master" AutoEventWireup="true" CodeBehind="driverRegister.aspx.cs" Inherits="RideNow.driver.driverRegister1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Driver Registration - RideNow</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500;700&display=swap" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/7.0.1/css/all.min.css" integrity="sha512-2SwdPD6INVrV/lHTZbO2nodKhrnDdJK9/kg2XD1r9uGqPo1cUbujc+IYdlYdEErWNu69gVcYgdxlmVmzTWnetw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <style>
        /* All your existing CSS remains the same */
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Roboto', sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            padding: 20px;
            line-height: 1.6;
        }

        .container {
            max-width: 800px;
            margin: 0 auto;
        }

        .header {
            text-align: center;
            margin-bottom: 30px;
            color: white;
        }

        .header h1 {
            font-size: 2.5rem;
            font-weight: 700;
            margin-bottom: 10px;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }

        .header p {
            font-size: 1.1rem;
            opacity: 0.9;
            font-weight: 300;
        }

        .registration-card {
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 40px rgba(0,0,0,0.1);
            overflow: hidden;
            margin-bottom: 30px;
        }

        .form-container {
            padding: 40px;
        }

        .form-section {
            margin-bottom: 40px;
        }

        .section-title {
            font-size: 1.3rem;
            font-weight: 600;
            color: #333;
            margin-bottom: 20px;
            padding-bottom: 10px;
            border-bottom: 2px solid #f0f0f0;
            position: relative;
        }

        .section-title::after {
            content: '';
            position: absolute;
            bottom: -2px;
            left: 0;
            width: 50px;
            height: 2px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        }

        .form-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }

        .form-group {
            position: relative;
            margin-bottom: 20px;
        }

        .form-group.full-width {
            grid-column: 1 / -1;
        }

        .form-group label {
            display: block;
            margin-bottom: 8px;
            font-weight: 500;
            color: #555;
            font-size: 0.9rem;
        }

        .form-group label.required::after {
            content: ' *';
            color: #e74c3c;
        }

        .input-wrapper {
            position: relative;
        }

        .form-control {
            width: 100%;
            padding: 15px 15px 15px 45px;
            border: 2px solid #e1e5e9;
            border-radius: 12px;
            font-size: 1rem;
            transition: all 0.3s ease;
            background: #fafbfc;
        }

        .form-control:focus {
            outline: none;
            border-color: #667eea;
            background: white;
            box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
        }
        
        /* Added styles for asp:DropDownList */
        select.form-control {
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='%23343a40' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M2 5l6 6 6-6'/%3e%3c/svg%3e");
            background-repeat: no-repeat;
            background-position: right 0.75rem center;
            background-size: 16px 12px;
            padding-right: 2.5rem;
        }

        .input-icon {
            position: absolute;
            left: 15px;
            top: 50%;
            transform: translateY(-50%);
            color: #8a94a6;
            font-size: 1.1rem;
        }

        .form-control:focus + .input-icon {
            color: #667eea;
        }
        
        .submit-section {
            text-align: center;
            margin-top: 40px;
            padding-top: 30px;
            border-top: 1px solid #e1e5e9;
        }

        .btn-submit {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border: none;
            padding: 18px 50px;
            font-size: 1.1rem;
            font-weight: 600;
            border-radius: 50px;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 8px 25px rgba(102, 126, 234, 0.3);
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        /* Styles for validation errors */
        .error-message {
            color: #e74c3c;
            font-size: 0.85rem;
            margin-top: 5px;
            display: block;
        }

        .form-group.error .form-control {
            border-color: #e74c3c;
        }

        .form-group.error .input-icon {
            color: #e74c3c;
        }

        @media (max-width: 768px) {
            .form-row {
                grid-template-columns: 1fr;
                gap: 0;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <div class="container">
        <div class="header">
            <h1><i class="fas fa-car"></i> Driver Registration</h1>
            <p>Join our network of professional drivers and start earning today</p>
        </div>

        <div class="registration-card">
            <div class="form-container">
                <asp:Literal ID="litMessage" runat="server"></asp:Literal>

                <div class="form-section">
                    <h2 class="section-title">Personal Information <a href="../register.aspx" style="position:absolute; right:0px; font-size: 1rem; font-weight: 500; text-decoration:none;"><i class="fa-solid fa-angle-left" style="font-size: 1rem; "></i>GO BACK</a></h2>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%=txtFullName.ClientID%>" class="required">Full Name</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter your full name"></asp:TextBox>
                                <i class="fas fa-user input-icon"></i>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="<%=txtEmail.ClientID%>" class="required">Email Address</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email" TextMode="Email"></asp:TextBox>
                                <i class="fas fa-envelope input-icon"></i>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%=txtPhone.ClientID%>" class="required">Phone Number</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Enter 10-digit phone number" MaxLength="10" TextMode="Number"></asp:TextBox>
                                <i class="fas fa-phone input-icon"></i>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="<%=txtDateOfBirth.ClientID%>" class="required">Date of Birth</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                <i class="fas fa-calendar input-icon"></i>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-section">
                    <h2 class="section-title">Account Security</h2>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%=txtPassword.ClientID%>" class="required">Password</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Create a strong password" TextMode="Password"></asp:TextBox>
                                <i class="fas fa-lock input-icon"></i>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="<%=txtConfirmPassword.ClientID%>" class="required">Confirm Password</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder="Confirm your password" TextMode="Password"></asp:TextBox>
                                <i class="fas fa-lock input-icon"></i>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-section">
                    <h2 class="section-title">License Information</h2>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%=txtLicenseNumber.ClientID%>" class="required">Driving License Number</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="form-control" placeholder="Enter license number"></asp:TextBox>
                                <i class="fas fa-id-card input-icon"></i>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="<%=txtLicenseExpiry.ClientID%>" class="required">License Expiry Date</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtLicenseExpiry" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                <i class="fas fa-calendar input-icon"></i>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="form-section">
                    <h2 class="section-title">Vehicle Information</h2>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%=txtMakeModel.ClientID%>" class="required">Make & Model</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtMakeModel" runat="server" CssClass="form-control" placeholder="e.g., Toyota Camry"></asp:TextBox>
                                <i class="fas fa-car input-icon"></i>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="<%=txtYear.ClientID%>" class="required">Year</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" placeholder="e.g., 2023" TextMode="Number" MaxLength="4"></asp:TextBox>
                                <i class="fas fa-calendar-week input-icon"></i>
                            </div>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group">
                            <label for="<%=txtLicensePlate.ClientID%>" class="required">License Plate</label>
                            <div class="input-wrapper">
                                <asp:TextBox ID="txtLicensePlate" runat="server" CssClass="form-control" placeholder="Enter license plate"></asp:TextBox>
                                <i class="fas fa-id-badge input-icon"></i>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="<%=ddlVehicleType.ClientID%>" class="required">Vehicle Type</label>
                            <div class="input-wrapper">
                                <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Select Vehicle Type" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Sedan" Value="Sedan"></asp:ListItem>
                                    <asp:ListItem Text="SUV" Value="SUV"></asp:ListItem>
                                    <asp:ListItem Text="Hatchback" Value="Hatchback"></asp:ListItem>
                                    <asp:ListItem Text="Luxury" Value="Luxury"></asp:ListItem>
                                </asp:DropDownList>
                                <i class="fas fa-car-side input-icon" style="z-index: 0;"></i>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="submit-section">
                    <asp:Button ID="btnRegisterDriver" runat="server" Text="Register as Driver" CssClass="btn-submit" OnClick="btnRegisterDriver_Click" />
                </div>
            </div>
        </div>
    </div>
    
    <%-- Make sure you have a jQuery reference, either on this page or in your MasterPage --%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    
    <%-- ======================= NEW VALIDATION SCRIPT ======================= --%>
    <script type="text/javascript">
        $(document).ready(function () {
            // Attach the validation function to the button's click event.
            $('#<%= btnRegisterDriver.ClientID %>').on('click', function (event) {
                if (!validateDriverForm()) {
                    event.preventDefault(); // Stop the postback if validation fails.
                }
            });

            function validateDriverForm() {
                let isValid = true;

                // Helper function to show an error message
                function showError(control, message) {
                    control.closest('.form-group').addClass('error');
                    // Add the error message span, but only if one doesn't already exist
                    if (control.closest('.form-group').find('.error-message').length === 0) {
                         control.closest('.input-wrapper').after('<span class="error-message">' + message + '</span>');
                    }
                    isValid = false;
                }

                // Helper function to clear an error message
                function clearError(control) {
                    control.closest('.form-group').removeClass('error');
                    control.closest('.form-group').find('.error-message').remove();
                }

                // --- Clear all previous errors before re-validating ---
                $('.form-container .form-control').each(function () {
                    clearError($(this));
                });

                // --- Define controls to validate ---
                const fullName = $('#<%= txtFullName.ClientID %>');
                const email = $('#<%= txtEmail.ClientID %>');
                const phone = $('#<%= txtPhone.ClientID %>');
                const dob = $('#<%= txtDateOfBirth.ClientID %>');
                const password = $('#<%= txtPassword.ClientID %>');
                const confirmPassword = $('#<%= txtConfirmPassword.ClientID %>');
                const licenseNumber = $('#<%= txtLicenseNumber.ClientID %>');
                const licenseExpiry = $('#<%= txtLicenseExpiry.ClientID %>');
                const makeModel = $('#<%= txtMakeModel.ClientID %>');
                const year = $('#<%= txtYear.ClientID %>');
                const licensePlate = $('#<%= txtLicensePlate.ClientID %>');
                const vehicleType = $('#<%= ddlVehicleType.ClientID %>');

                // --- Validation Logic ---

                // 1. Full Name
                if (fullName.val().trim() === '') showError(fullName, 'Full Name is required.');

                // 2. Email
                const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (email.val().trim() === '') {
                    showError(email, 'Email Address is required.');
                } else if (!emailRegex.test(email.val().trim())) {
                    showError(email, 'Please enter a valid email address.');
                }

                // 3. Phone Number
                const phoneRegex = /^\d{10}$/;
                if (phone.val().trim() === '') {
                    showError(phone, 'Phone Number is required.');
                } else if (!phoneRegex.test(phone.val().trim())) {
                    showError(phone, 'Please enter a valid 10-digit phone number.');
                }

                // 4. Date of Birth
                if (dob.val().trim() === '') {
                    showError(dob, 'Date of Birth is required.');
                } else {
                    const birthDate = new Date(dob.val());
                    const today = new Date();
                    let age = today.getFullYear() - birthDate.getFullYear();
                    const monthDiff = today.getMonth() - birthDate.getMonth();
                    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
                        age--;
                    }
                    if (age < 18) {
                        showError(dob, 'You must be at least 18 years old.');
                    }
                }

                // 5. Password
                if (password.val() === '') {
                    showError(password, 'Password is required.');
                } else if (password.val().length < 6) {
                    showError(password, 'Password must be at least 6 characters.');
                }

                // 6. Confirm Password
                if (confirmPassword.val() === '') {
                    showError(confirmPassword, 'Please confirm your password.');
                } else if (password.val() !== confirmPassword.val()) {
                    showError(confirmPassword, 'Passwords do not match.');
                }

                // 7. License Number
                if (licenseNumber.val().trim() === '') showError(licenseNumber, 'License Number is required.');

                // 8. License Expiry
                if (licenseExpiry.val().trim() === '') {
                    showError(licenseExpiry, 'License Expiry Date is required.');
                } else {
                    const expiryDate = new Date(licenseExpiry.val());
                    const today = new Date();
                    today.setHours(0, 0, 0, 0); // Set to start of day for accurate comparison
                    if (expiryDate < today) {
                        showError(licenseExpiry, 'Your license cannot be expired.');
                    }
                }

                // 9. Make & Model
                if (makeModel.val().trim() === '') showError(makeModel, 'Make & Model is required.');

                // 10. Year
                if (year.val().trim() === '') showError(year, 'Vehicle Year is required.');

                // 11. License Plate
                if (licensePlate.val().trim() === '') showError(licensePlate, 'License Plate is required.');

                // 12. Vehicle Type
                if (vehicleType.val() === '') showError(vehicleType, 'Please select a Vehicle Type.');


                if (!isValid) {
                    // Optional: Scroll to the first error on the page
                    const firstError = $('.form-group.error').first();
                    if (firstError.length) {
                        $('html, body').animate({
                            scrollTop: firstError.offset().top - 20
                        }, 500);
                    }
                }

                return isValid;
            }
        });
    </script>
</asp:Content>