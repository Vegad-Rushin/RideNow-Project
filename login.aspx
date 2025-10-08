<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="RideNow.login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder1">

        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"><title>RideNow - Log in</title>
            <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">
            <link rel="stylesheet" href="css/bootstrap.css">
            <link rel="stylesheet" href="css/font-awesome.min.css">
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
                                <li><a href="register.aspx" class="signup-btn">Sign up</a></li>
                            </ul>
                        </nav>
                    </div>
                </div>
            </header>
            <main class="auth-wrapper">
                <div class="auth-background">
                    <div class="auth-overlay">
                    </div>
                </div>
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-lg-5 col-md-7">
                            <section class="auth-card scroll-animate-scale">
                                <div class="auth-header">
                                    <h1 class="auth-title">Welcome back</h1>
                                    <p class="auth-subtitle">
                                        Log in with your email or phone to continue</p>
                                </div>
                                
                                <div class="auth-form">
                                    <div class="form-group">
                                        <label for="<%=txtIdentifier.ClientID%>">
                                        Email or phone</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fa fa-user"></i></span>
                                            </div>
                                            <asp:TextBox ID="txtIdentifier" runat="server" CssClass="form-control" placeholder="you@example.com or +1 555 000 0000"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="<%=txtPassword.ClientID%>">
                                        Password</label>
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fa fa-lock"></i></span>
                                            </div>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-row between">
                                        <asp:CheckBox ID="chkRememberMe" runat="server" Text=" Remember me" CssClass="checkbox" />
                                        <a class="link-small" href="#">Forgot password?</a>
                                    </div>
                                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Log in" CssClass="btn btn-primary btn-block">
                                    </asp:Button>
                                    <i class="fa fa-sign-in" style="display:none;"></i> 
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
                                    <span>New to RideNow?</span> <a href="register.aspx" class="link-inline">Create an account</a>
                                </div>
                            </section>
                        </div>
                    </div>
                </div>
            </main>

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

                    // Observe all scroll animate elements
                    document.querySelectorAll('.scroll-animate, .scroll-animate-left, .scroll-animate-right, .scroll-animate-scale, .scroll-animate-rotate').forEach(el => {
                        observer.observe(el);
                    });

                    // Coming soon functionality for buttons
                    $('a[href="#"], button:not(.coming-soon-close):not(.btn-primary)').on('click', function (e) {
                        e.preventDefault();
                        showComingSoon();
                    });
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
