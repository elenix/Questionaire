<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bootstrap.aspx.cs" Inherits="Questionaire.Bootstrap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Try and Error!</title>
    <link href="Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleB.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container-fluid">
        <a class="navbar-brand" href="#">QUESTIONARE</a>
        </div>
    </nav>
        <div class="container-fluid">
            <div class="row">
                <div class="jumbotron col-md-3" style="margin-left: 10px; margin-top: 100px;">
                    <div class="row">
                        <div class="col">
                            <ul class="nav nav-pills nav-stacked">
                                <li class="active"><a class="dropdown-toggle" data-toggle="dropdown" href="#" role="button"
                                    aria-haspopup="true" aria-expanded="false">Module <span class="caret"></span></a>
                                    <ul class="dropdown-menu" style="width: 100%; text-align: center;">
                                        <li><a href="#">Main</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Employee</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Payroll</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Statutory Reporting</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Attendance</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Leave</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Benefit</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Financial Interface</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Data Import</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Training</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Performance</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Staff Inventory</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Transport</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Manpower</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">Recruitment</a></li>
                                        <li role="separator" class="divider"></li>
                                        <li><a href="#">ESOS</a></li>
                                    </ul>
                                </li>
                                <li class="active"><a href="#">Questionare Setup</a></li>
                                <li class="active"><a href="#">Input</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="jumbotron col-md-8" style="margin-left: 10px; margin-top: 100px;">
                    <form>
                    <div class="form-group row">
                        <label for="example-text-input" class="col-xs-3 col-form-label">
                            Reference Code:</label>
                        <div class="col-xs-8">
                            <input class="form-control" type="text" value="Auto-Generate(Unique)" id="example-text-input">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-text-input" class="col-xs-3 col-form-label">
                            Sequence:</label>
                        <div class="col-xs-8">
                            <input class="form-control" type="text" value="" id="Text1">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-text-input" class="col-xs-3 col-form-label">
                            Question:<br />
                            <small>(Content)</small></label>
                        <div class="col-xs-8">
                            <input class="form-control" type="text" value="Type Here.." id="Text2">
                            <asp:Button ID="Button1" runat="server" Text="Add Question" CssClass="btn btn-info form-inline" Style="margin-top:10px; width:100%" />
                        </div>
                    </div><br />
                    </form>
                    <div class="container">
                    <table id="question-table" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Reference code</th>                                                                   
                                <th>Seq</th>                                
                                <th>Question </th>
                                <th>Option</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1000000111</td>
                                <td>1</td> 
                                <td>Please list down you company headcount :</td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Edit" CssClass="btn btn-success" />
                                    <asp:Button ID="Button3" runat="server" Text="Delete" CssClass="btn btn-danger"/> 
                                </td>        
                            </tr>
                            <tr>
                               <td>1000000112</td>
                               <td>2</td> 
                               <td>Please list down you company headcount :</td>
                               <td>
                                    <asp:Button ID="Button4" runat="server" Text="Edit" CssClass="btn btn-success" />
                                    <asp:Button ID="Button5" runat="server" Text="Delete" CssClass="btn btn-danger"/> 
                                </td>           
                            </tr>
                            <tr>
                               <td>1000000113</td>
                               <td>3</td> 
                               <td>Please list down you company headcount :</td>
                               <td>
                                    <asp:Button ID="Button6" runat="server" Text="Edit" CssClass="btn btn-success" />
                                    <asp:Button ID="Button7" runat="server" Text="Delete" CssClass="btn btn-danger"/> 
                                </td>           
                            </tr>
                        </tbody>
                    </table>
                </div>
        </div>
    </div>
    </div>
    <div class="footer">
        <h1>
            <span class="glyphicon glyphicon-copyright-mark"></span>PPAP!</h1>
    </div>
    </form>
</body>
</html>
