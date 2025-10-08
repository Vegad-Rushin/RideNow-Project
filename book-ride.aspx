<%@ Page Title="" Language="C#" MasterPageFile="~/book-ride.Master" AutoEventWireup="true" CodeBehind="book-ride.aspx.cs" Inherits="RideNow.book_ride1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <!DOCTYPE html>
    <html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
        <meta charset="UTF-8">
        <title>RideNow - Book Your Ride</title>
        <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/7.0.1/css/all.min.css" integrity="sha512-2SwdPD6INVrV/lHTZbO2nodKhrnDdJK9/kg2XD1r9uGqPo1cUbujc+IYdlYdEErWNu69gVcYgdxlmVmzTWnetw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
        <link rel="stylesheet" href="css/linearicons.css">
        <link rel="stylesheet" href="css/font-awesome.min.css">
        <link rel="stylesheet" href="css/bootstrap.css">
        <link rel="stylesheet" href="css/main.css">
        <link rel="stylesheet" href="css/intro.css">
        <link rel="stylesheet" href="css/auth.css">
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
                            <li id="liLogin" runat="server"><a href="login.aspx">Log in</a></li>

                            <%-- For Driver --%>
                            <li id="liDriverDashboard" runat="server"><a href="driver/dashboard.aspx">Dashboard</a></li>
                            <li id="liDriverProfile" runat="server"><a href="driver/profile.aspx">Profile</a></li>

                            <li id="liDashboard" runat="server"><a href="user-dashboard.aspx">Dashboard</a></li>
                            <li id="liLogout" runat="server">
                                <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click">Logout</asp:LinkButton></li>

                            <li id="liGetStarted" runat="server"><a href="register.aspx" class="login-btn">Get Started</a></li>
                        </ul>
                    </nav>
                </div>
            </div>
        </header>

        <section class="booking-hero">
            <div class="hero-background">
            </div>
            <div class="container">
                <div class="row align-items-center">
                    <div class="col-lg-6">
                        <div class="hero-content" style="margin-right: 10em;">
                            <h1 class="hero-title">Book Your Perfect Ride</h1>
                            <p class="hero-subtitle">
                                Experience seamless transportation with our premium ride booking service. Fast, reliable, and affordable rides tailored to your needs.
                            </p>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="booking-card-wrapper">
                            <div class="booking-card">
                                <div class="card-header">
                                    <h2 class="card-title"><i class="fa fa-car"></i>Ride Booking <a href="../user-dashboard.aspx" style="position: absolute; font-size: 16px; right: 50px;"><i class="fa-solid fa-angle-left" style="font-size: 16px;"></i>BACK</a></h2>
                                </div>
                                <!-- ASP.NET Form Starts Here -->
                                <div class="booking-form">
                                    <div class="form-row">
                                        <div class="form-group">
                                            <label for="<%=txtName.ClientID%>">Full name</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-user"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Full name"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%=txtPhone.ClientID%>">Phone</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-phone"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Phone"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group">
                                            <label for="<%=ddlVehicleType.ClientID%>">Vehicle type</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-car"></i></span>
                                                </div>
                                                <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="">Choose type</asp:ListItem>
                                                    <asp:ListItem Value="Sedan">Sedan</asp:ListItem>
                                                    <asp:ListItem Value="SUV">SUV</asp:ListItem>
                                                    <asp:ListItem Value="Hatchback">Hatchback</asp:ListItem>
                                                    <asp:ListItem Value="Luxury">Luxury</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label for="<%=txtDistance.ClientID%>">Distance (in KM)</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-road"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtDistance" runat="server" CssClass="form-control" TextMode="Number" placeholder="e.g., 10"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-row">
                                        <div class="form-group">
                                            <label for="<%=txtFare.ClientID%>">Total fare</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-dollar"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtFare" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%=txtPickupTime.ClientID%>">Pickup time</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-calendar"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtPickupTime" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group">
                                            <label for="<%=txtPickupAddress.ClientID%>">Pickup address</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-map-marker"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtPickupAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Pickup address"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="<%=txtDropoffAddress.ClientID%>">Dropoff address</label>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="fa fa-flag"></i></span>
                                                </div>
                                                <asp:TextBox ID="txtDropoffAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Dropoff address"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <asp:Button ID="btnBookRide" runat="server" OnClick="btnBookRide_Click" Text="Book Ride" CssClass="btn btn-primary btn-lg" />
                                    </div>
                                </div>
                                <!-- ASP.NET Form Ends Here -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Other Sections & Footer -->
        <script>
            $(document).ready(function () {
                // Add this inside your existing $(document).ready function
                $('#<%=txtDistance.ClientID%>').on('keyup', function () {
                    const distance = parseFloat($(this).val());
                    const ratePerKm = 15; // Let's assume the rate is ₹15 per KM

                    if (!isNaN(distance) && distance > 0) {
                        const totalFare = distance * ratePerKm;
                        $('#<%=txtFare.ClientID%>').val(totalFare.toFixed(2));
                    } else {
                        $('#<%=txtFare.ClientID%>').val('0.00');
                    }
                });
            });
        </script>
        <script src="js/vendor/jquery-2.2.4.min.js"></script>
        <script src="js/vendor/bootstrap.min.js"></script>
        <script src="js/main.js"></script>
    </body>
    </html>
</asp:Content>

