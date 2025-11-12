<%@ Page Title="" Language="C#" MasterPageFile="~/user-dashboard.Master" AutoEventWireup="true" CodeBehind="user-dashboard.aspx.cs" Inherits="RideNow.user_dashboard1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">

        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"><title>RideNow - Dashboard</title>
            <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">
            <link rel="stylesheet" href="css/bootstrap.css">
            <link rel="stylesheet" href="css/font-awesome.min.css">
            <link rel="stylesheet" href="css/main.css">
            <link rel="stylesheet" href="css/intro.css">
            <link rel="stylesheet" href="css/auth.css">

            <style>

                html, body {
                    overflow-x : hidden;
                    width:100%;
                }

                .dashboard-stats {
                    display:flex;
                    justify-content:center;
                    align-items:center;
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
                                <li><a href="user-dashboard.aspx" class="active">Dashboard</a></li>
                                <li><asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click" CssClass="signup-btn">Logout</asp:LinkButton></li>
                            </ul>
                        </nav>
                    </div>
                </div>
            </header>

            <!-- Hero Section -->
            <section class="dashboard-hero">
                <div class="container">
                    <div class="row align-items-center">
                        <div class="col-lg-6">
                            <div class="welcome-content">
                                <h1 class="welcome-title">Welcome back, <asp:Literal ID="litUserNameWelcome" runat="server"></asp:Literal>!</h1>
                                <p class="welcome-subtitle">
                                    Ready for your next adventure? Let's get you where you need to go.</p>
                                <div class="quick-actions">
                                    <a href="book-ride.aspx" class="btn btn-primary btn-lg"><i class="fa fa-car"></i>Book a Ride </a>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="dashboard-illustration">
                                <div class="ride-animation">
                                    <div class="car-icon">
                                        <i class="fa fa-car"></i>
                                    </div>
                                    <div class="route-line">
                                    </div>
                                    <div class="destination-icon">
                                        <i class="fa fa-map-marker"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <!-- Stats Section -->
            <section class="dashboard-stats">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-lg-3 col-md-6">
                            <div class="stat-card scroll-animate">
                                <div class="stat-icon">
                                    <i class="fa fa-car"></i>
                                </div>
                                <div class="stat-content">
                                    <h3><asp:Literal ID="litTotalRides" runat="server">0</asp:Literal></h3>
                                    <p>
                                        Total Rides
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="stat-card scroll-animate delay-1">
                                <div class="stat-icon">
                                    <i class="fa fa-inr"></i>
                                </div>
                                <div class="stat-content">
                                    <h3><asp:Literal ID="litTotalSpent" runat="server">0.00</asp:Literal></h3>
                                    <p>
                                        Total Spent</p>
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-lg-3 col-md-6">
                            <div class="stat-card scroll-animate delay-2">
                                <div class="stat-icon">
                                    <i class="fa fa-star"></i>
                                </div>
                                <div class="stat-content">
                                    <h3>4.8</h3>
                                    <p>
                                        Average Rating</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-6">
                            <div class="stat-card scroll-animate delay-3">
                                <div class="stat-icon">
                                    <i class="fa fa-trophy"></i>
                                </div>
                                <div class="stat-content">
                                    <h3>Gold</h3>
                                    <p>
                                        Member Level</p>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </section>

            <!-- Main Content -->
            <section class="dashboard-content">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-4">
                            <!-- Profile Card -->
                            <div class="dashboard-card profile-card">
                                <div class="card-header">
                                    <h3><i class="fa fa-user"></i>Profile</h3>
                                </div>
                                <div class="card-body">
                                    <div class="profile-avatar">
                                        <div class="avatar-circle">
                                            <i class="fa fa-user"></i>
                                        </div>
                                        <div class="profile-info">
                                            <h4><asp:Literal ID="litUserNameProfile" runat="server"></asp:Literal></h4>
                                            <p>
                                                Gold Member</p>
                                        </div>
                                    </div>
                                    <div class="profile-details">
                                        <div class="detail-item">
                                            <i class="fa fa-phone"></i><asp:Literal ID="litUserPhone" runat="server"></asp:Literal>
                                        </div>
                                    <div class="detail-item">
                                            <i class="fa fa-envelope"></i><asp:Literal ID="litUserEmail" runat="server"></asp:Literal>
                                    </div>
                                        
                                    </div>
                                    <a href="editProfile.aspx" class="btn btn-outline btn-block">
                                        <i class="fa fa-edit"></i> Edit Profile
                                    </a>
                                </div>
                            </div>

                            <!-- Quick Actions Card -->
                            <div class="dashboard-card quick-actions-card">
                                <div class="card-header">
                                    <h3><i class="fa fa-bolt"></i>Quick Actions</h3>
                                </div>
                                <div class="card-body">
                                    <div class="action-buttons">
                                        <a href="book-ride.aspx" class="action-btn">
                                            <i class="fa fa-car"></i><span>Book Ride</span>
                                        </a>
                                        <a href="editProfile.aspx" class="action-btn">
                                            <i class="fa fa-cog"></i><span>Settings</span>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-8">
                            <!-- Recent Rides Card -->
                            <div id="rideStatusNotification"></div> <%-- Add this line --%>
                            <div class="dashboard-card recent-rides-card">
                            <div class="card-header">
                                <h3><i class="fa fa-history"></i>Recent Rides</h3>
                                <a href="#" class="view-all">View All</a>
                            </div>
                            <div class="card-body">
                                <div class="rides-list">
                                    <asp:Repeater ID="rptRecentRides" runat="server">
                                        <ItemTemplate>
                                            <div class="ride-item">
                                                <div class="ride-icon">
                                                    <i class="fa fa-car"></i>
                                                </div>
                                                <div class="ride-details">
                                                    <div class="ride-route">
                                                        <span class="from"><%# Eval("pickup_address") %></span> <i class="fa fa-arrow-right"></i><span class="to"><%# Eval("dropoff_address") %></span>
                                                    </div>
                                                    <div class="ride-meta">
                                                        <span class="date"><%# Convert.ToDateTime(Eval("pickup_time")).ToString("MMM dd, yyyy") %></span>
                                                        <span class="time"><%# Convert.ToDateTime(Eval("pickup_time")).ToString("h:mm tt") %></span>
                                                        <span class="fare"><%# string.Format("&#8377; {0:F2}", Eval("total_fare")) %></span>
                                                    </div>
                                                </div>
                                                <div class="ride-status completed">
                                                    <i class="fa fa-check"></i><span><%# Eval("booking_status") %></span>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Literal ID="litNoRides" runat="server" Visible="false"></asp:Literal>
                                </div>
                            </div>
                        </div>

                        </div>
                    </div>
                </div>
            </section>
            <footer class="footer-section">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="footer-widget">
                                <h3>RideNow</h3>
                                <p>
                                    Your trusted partner for safe, reliable, and affordable transportation services.</p>
                                <div class="social-links">
                                    <a href="#"><i class="fa fa-facebook"></i></a><a href="#"><i class="fa fa-twitter"></i></a><a href="#"><i class="fa fa-instagram"></i></a><a href="#"><i class="fa fa-linkedin"></i></a>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="footer-widget">
                                <h4>Services</h4>
                                <ul>
                                    <li><a href="#">City Rides</a></li>
                                    <li><a href="#">Airport Transfer</a></li>
                                    <li><a href="#">Event Transport</a></li>
                                    <li><a href="#">Business Travel</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="footer-widget">
                                <h4>Company</h4>
                                <ul>
                                    <li><a href="#">About Us</a></li>
                                    <li><a href="#">Careers</a></li>
                                    <li><a href="#">Press</a></li>
                                    <li><a href="#">Blog</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="footer-widget">
                                <h4>Support</h4>
                                <ul>
                                    <li><a href="#">Help Center</a></li>
                                    <li><a href="#">Safety</a></li>
                                    <li><a href="#">Terms</a></li>
                                    <li><a href="#">Privacy</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="footer-widget">
                                <h4>Download</h4>
                                <div class="app-links">
                                    <a href="#" class="app-link"><i class="fa fa-apple"></i><span>App Store</span> </a><a href="#" class="app-link"><i class="fa fa-android"></i><span>Google Play</span> </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="footer-bottom">
                        <p>
                            &copy;
                            <script>document.write(new Date().getFullYear());</script>
                            RideNow. All rights reserved.</p>
                    </div>
                </div>
            </footer>

            <!-- Coming Soon Modal -->
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
                $(document).ready(function ()
                {
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

                    // Observe all scroll animate elements
                    document.querySelectorAll('.scroll-animate, .scroll-animate-left, .scroll-animate-right, .scroll-animate-scale, .scroll-animate-rotate').forEach(el => {
                        observer.observe(el);
                    });

                    // Coming soon functionality for buttons
                    $('a[href="#"], button:not(.coming-soon-close):not(.btn-primary)').on('click', function (e) {
                        e.preventDefault();
                        showComingSoon();
                    });


                    // Function to check for ride status updates
                    function checkForRideUpdates() {
                        $.ajax({
                            type: "POST",
                            url: "user-dashboard.aspx/GetRideStatusUpdate", // Calls our C# WebMethod
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                const rideData = response.d;
                                if (rideData) {
                                    // If we get data, a ride was accepted. Show the notification.
                                    const notificationHTML = `
                    <div class="alert alert-success" role="alert" style="display:none; margin-bottom: 20px;">
                        <h4 class="alert-heading">Your Ride is on the Way!</h4>
                        <p>
                            <strong>${rideData.driverName}</strong> has accepted your request. 
                            Car: <strong>${rideData.vehicle}</strong> (License Plate: <strong>${rideData.license}</strong>).
                        </p>
                        <hr>
                        
                    </div>`;

                                    $('#rideStatusNotification').html(notificationHTML);
                                    $('#rideStatusNotification .alert').slideDown();

                                    // Optional: Stop checking once an update is found for this session
                                    // clearInterval(rideCheckInterval); 
                                }
                            },
                            error: function (err) {
                                console.log("Error checking for ride updates.");
                            }
                        });
                    }

                    // Start checking for updates every 5 seconds (5000 milliseconds)
                    const rideCheckInterval = setInterval(checkForRideUpdates, 5000);

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

                // Close modal when clicking outside
                $(document).on('click', '.coming-soon-modal', function (e) {
                    if (e.target === this) {
                        closeComingSoon();
                    }
                });
            </script>
        </body>
        </html>
</asp:Content>
