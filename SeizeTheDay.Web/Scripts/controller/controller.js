
/*
These controllers that are in the below belongs to the Admin Web Pages like AddForum,AddForumPost
*/

//Menu Controller
app.controller('MenuCtrl', ['$scope', 'menuService',
    function ($scope, menuService) {

        $scope.init = function () {
            $scope.getMenuList();
        };

        $scope.getMenuList = function () {
            menuService.getMenuList().then(function (result) {
                $scope.lstMenu = result.data;
                $scope.lstParentMenu = [];
                $scope.lstSubMenu = [];

                //make a main menu and a sub menu different list
                for (var i = 0; i < result.data.length; i++) {
                    if (result.data[i].ParentModuleID === 0) {
                        $scope.lstParentMenu.push(result.data[i]);
                    }
                    else {
                        $scope.lstSubMenu.push(result.data[i]);
                    }
                }

                setTimeout(function () {
                    $scope.$apply(function () {
                        //Set submenu list in main menu 
                        for (var j = 0; j < $scope.lstParentMenu.length; j++) {
                            $scope.lstParentMenu[j].Submenu = [];

                            for (var k = 0; k < $scope.lstSubMenu.length; k++) {
                                if ($scope.lstParentMenu[j].ID === $scope.lstSubMenu[k].ParentModuleID) {
                                    $scope.lstParentMenu[j].Submenu.push($scope.lstSubMenu[k]);
                                    console.log($scope.lstSubMenu[k]);
                                }
                            }
                        }
                    });
                }, 500);
            }, function (error) {
                $scope.status = 'Unable to load menu data: ' + error.message;
            });
        };
        $scope.init();
    }]);

//User Controller
app.controller('UserCtrl', ['$scope', 'userService', 'roleService', 'userTypeService',
    function ($scope, userService, roleService, userTypeService) {

        //Initialize data
        $scope.init = function () {
            $scope.model = {
                Id: '',
                Email: '',
                EmailConfirmed: false,
                PasswordHash: '',
                SecurityStamp: '',
                PhoneNumber: '',
                PhoneNumberConfirmed: false,
                TwoFactorEnabled: false,
                LockoutEndDateUtc: null,
                LockoutEnabled: false,
                AccessFailedCount: null,
                UserName: '',
                Status: '',

                //UserInfo
                FirstName: '',
                LastName: '',
                BirthDate: null,
                Address: '',
                PhotoPath: '',
                FacebookLink: '',
                TwitterLink: '',
                SkypeID: '',
                IsDefault: false,
                IsDeleted: false,
                LastLoginDate: null,
                RegisteredDate: null,
                InsertBy: null,
                LastModified: null,
                LastModifiedBy: null,
                IsActive: false,
                CoverPhotoPath: null,
                UserCity: null,
                CountryID: null,
                UserTypeID: null,
                UserTask: null,
                TagUserName: null,
                TagColor: null,
                UserTypeName: null,
                RoleID: [],
                CPassword: ''
            };

            //for User Detail
            $scope.flgflgUserDetail = false;

            //for User list
            $scope.flgTable = true;

            //for display message of user   
            $scope.flgMessage = false;

            $scope.flgUser = true;
            $scope.showCreate = false;
            $scope.showEdit = false;

            //for showing message
            $scope.flgMessage1 = false;
            $scope.message = "";
            $scope.message1 = "";

            //for User link
            $scope.UserState = "";
            $scope.getAllUserType();
            $scope.getAllUser();
            $scope.getAllRoles();

            //Defined checked Role
            $scope.checkedRole = {
                role: [],
                getRoleId: []
            };
        };

        //hide message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");

            $scope.flgMessage1 = false;
            $("#message1").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon1").removeClass("fa-check").removeClass("fa-ban");
        };

        //GetAllUser
        $scope.getAllUser = function () {

            var table = $("#userTable").DataTable();
            table.clear();
            table.destroy();

            userService.getUsers()
                .then(function (result) {
                    $scope.lstUsers = result.data;
                    setTimeout(function () {
                        $('#userTable').DataTable({
                            "aoColumnDefs": [{
                                "bSortable": false,
                                "aTargets": [-1]
                            }],
                            "paging": true,
                            "lengthChange": true,
                            "searching": true,
                            "ordering": true,
                            "info": true,
                            "autoWidth": false
                        });
                    }, 500);
                }, function (error) {
                    $scope.status = 'Unable to load user data: ' + error.message;
                });
        };

        $scope.getAllUserType = function () {
            userTypeService.getTypes()
                .then(function (result) {
                    $scope.lstUserType = result.data;
                });
        };

        //Open user form
        $scope.addUser = function () {
            //make table flg false for dispaly message
            $scope.flgTable = false;
            $scope.showCreate = true;
            $scope.showEdit = false;
            $scope.UserState = "> Add User";
        };

        //Edit User
        $scope.editUser = function (obj) {
            $scope.UserState = "> Edit User";
            $scope.model.Id = obj.Id;
            $scope.model.Email = obj.Email;
            $scope.model.EmailConfirmed = obj.EmailConfirmed;
            $scope.model.PasswordHash = obj.PasswordHash;
            $scope.model.SecurityStamp = obj.SecurityStamp;
            $scope.model.PhoneNumber = obj.PhoneNumber;
            $scope.model.PhoneNumberConfirmed = obj.PhoneNumberConfirmed;
            $scope.model.TwoFactorEnabled = obj.TwoFactorEnabled;
            $scope.model.LockoutEndDateUtc = obj.LockoutEndDateUtc;
            $scope.model.LockoutEnabled = obj.LockoutEnabled;
            $scope.model.AccessFailedCount = obj.AccessFailedCount;
            $scope.model.UserName = obj.UserName;
            $scope.model.Status = obj.Status;

            //UserInfo
            $scope.model.FirstName = obj.FirstName;
            $scope.model.LastName = obj.LastName;
            $scope.model.BirthDate = obj.BirthDate;
            $scope.model.Address = obj.Address;
            $scope.model.PhotoPath = obj.PhotoPath;
            $scope.model.FacebookLink = obj.FacebookLink;
            $scope.model.TwitterLink = obj.TwitterLink;
            $scope.model.SkypeID = obj.SkypeID;
            $scope.model.IsDefault = obj.IsDefault;
            $scope.model.IsDeleted = obj.IsDeleted;
            $scope.model.LastLoginDate = obj.LastLoginDate;
            $scope.model.RegisteredDate = obj.RegisteredDate;
            $scope.model.InsertBy = obj.InsertBy;
            $scope.model.LastModified = obj.LastModified;
            $scope.model.LastModifiedBy = obj.LastModifiedBy;
            $scope.model.IsActive = obj.IsActive;
            $scope.model.CoverPhotoPath = obj.CoverPhotoPath;
            $scope.model.UserCity = obj.UserCity;
            $scope.model.CountryID = obj.CountryID;
            $scope.model.UserTypeID = obj.UserTypeID;
            $scope.model.UserTask = obj.UserTask;
            $scope.model.TagUserName = obj.TagUserName;
            $scope.model.TagColor = obj.TagColor;
            $scope.model.UserTypeName = obj.UserTypeName;
            $scope.model.RoleID = obj.RoleID;
            $scope.flgTable = false;
            $scope.showCreate = false;
            $scope.showEdit = true;


            //Editing User Roles
            if (obj.RoleID.length > 1) {
                for (var i = 0; i < obj.RoleID.length; i++) {
                    for (var j = 0; j < $scope.lstRoles.length; j++) {
                        if ($scope.lstRoles[j].Id === obj.RoleID[j]) {
                            if (!_.contains($scope.checkedRole.role, $scope.lstRoles[j])) {
                                $scope.checkedRole.role.push($scope.lstRoles[j]);
                                $scope.checkedRole.getRoleId.push($scope.lstRoles[j].Id);
                            }
                        }
                    }
                }
            }
            else if (obj.RoleID.length == 1) {
                for (var i = 0; i < obj.RoleID.length; i++) {
                    for (var j = 0; j < $scope.lstRoles.length; j++) {
                        if ($scope.lstRoles[j].Id === obj.RoleID[0]) {
                            if (!_.contains($scope.checkedRole.role, $scope.lstRoles[j])) {
                                $scope.checkedRole.role.push($scope.lstRoles[j]);
                                $scope.checkedRole.getRoleId.push($scope.lstRoles[j].Id);
                            }
                        }
                    }
                }
            }

            //For Uploading User profile picture
            $scope.Url = "/api/UploadFile?UserId=" + obj.ID;
        };

        //Create/update User
        $scope.createUser = function (obj) {
            $scope.hideMessage();
            if ($scope.showCreate === true) {
                //Check password and confirm password is same
                if (obj.PasswordHash !== obj.CPassword) {
                    $scope.flgMessage = true;
                    $scope.message = "Password and Confirm Password must be same.";
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
                else {
                    //getting roles that you have just selected
                    obj.RoleID = []; //cleaning the previous values
                    for (var i = 0; i < $scope.checkedRole.role.length; i++) {
                        obj.RoleID.push($scope.checkedRole.role[i].Id);
                        console.log($scope.checkedRole.role[i].Id);
                    }
                    userService.insertUser(obj).then(function (result) {
                        if (result.data.success === 1) {
                            $scope.flgMessage = true;
                            $scope.message = result.data.message;
                            $("#message").addClass("alert alert-success");
                            $("#icon").addClass("fa-thumbs-up");
                            $scope.getAllUser();
                            $scope.reset();
                        }
                        else {
                            $scope.flgMessage = true;
                            $scope.message = result.data.message;
                            $("#message").addClass("alert alert-danger");
                            $("#icon").addClass("fa-times ");
                        }
                    });
                }
            }
            else {

                //getting roles that you have just selected
                obj.RoleID = []; //cleaning the previous values
                for (var i = 0; i < $scope.checkedRole.role.length; i++) {
                    obj.RoleID.push($scope.checkedRole.role[i].Id);
                    console.log($scope.checkedRole.role[i].Id);
                }

                userService.insertUser(obj).then(function (result) {
                    if (result.data.success === 1) {
                        $scope.flgMessage = true;
                        $scope.message = result.data.message;
                        $("#message").addClass("alert alert-success");
                        $("#icon").addClass("fa-thumbs-up");
                        $scope.getAllUser();
                        $scope.reset();
                    }
                    else {
                        $scope.flgMessage = true;
                        $scope.message = result.data.message;
                        $("#message").addClass("alert alert-danger");
                        $("#icon").addClass("glyphicon glyphicon-warning-sign ");
                    }
                });
            }
        };

        //Delete User
        $scope.deleteUser = function (obj) {
            $scope.hideMessage();
            params = {
                ID: obj.Id
            }
            userService.deleteUser(obj).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllUser();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Get all roles
        $scope.getAllRoles = function () {
            roleService.getRoles()
                .then(function (result) {
                    $scope.lstRoles = result.data;
                });
        };

        function formatDate(d) {
            date = new Date(d);
            var dd = date.getDate();
            var mm = date.getMonth() + 1;
            var yyyy = date.getFullYear();
            if (dd < 10) { dd = '0' + dd }
            if (mm < 10) { mm = '0' + mm };
            return d = yyyy + '-' + mm + '-' + dd;
        }

        //Reset data
        $scope.reset = function () {
            $scope.checkedRole.role = [];
            $scope.UserState = "";
            $scope.flgTable = true;
            $scope.flgflgUserDetail = false;
            $scope.flgUserDetail = false;
            $scope.flgUser = true;
            $scope.model = {
                Id: '',
                Email: '',
                EmailConfirmed: false,
                PasswordHash: '',
                SecurityStamp: '',
                PhoneNumber: '',
                PhoneNumberConfirmed: false,
                TwoFactorEnabled: false,
                LockoutEndDateUtc: null,
                LockoutEnabled: false,
                AccessFailedCount: null,
                UserName: '',
                Status: '',

                //UserInfo
                FirstName: '',
                LastName: '',
                BirthDate: null,
                Address: '',
                PhotoPath: '',
                FacebookLink: '',
                TwitterLink: '',
                SkypeID: '',
                IsDefault: false,
                IsDeleted: false,
                LastLoginDate: null,
                RegisteredDate: null,
                InsertBy: null,
                LastModified: null,
                LastModifiedBy: null,
                IsActive: false,
                CoverPhotoPath: '',
                UserCity: '',
                CountryID: null,
                UserTypeID: null,
                UserTask: '',
                TagUserName: '',
                TagColor: '',
                UserTypeName: '',
                RoleID: [],
                CPassword: ''
            },

                $("#liTab_2").removeClass("active");
            $("#tab_2").removeClass("active");
            $("#liTab_1").addClass("active");
            $("#tab_1").addClass("active");
        };

        //Set Image
        $scope.Image = function (e) {
            $("#imgs").attr('src', URL.createObjectURL(e.target.files[0]));
        };

        $scope.init();
    }]);

//Role Controller
app.controller('RoleCtrl', ['$scope', 'roleService',
    function ($scope, roleService) {
        //Initialize data
        $scope.init = function () {

            $scope.model = {
                ID: '',
                Name: '',
                UserPermission: [],
            };

            //for Display Role list
            $scope.flgTable = true;

            //for Display Message
            $scope.flgMessage = false;

            //for message
            $scope.message = "";

            //for Role Link
            $scope.UserState = "";

            $scope.getAllRoles();

            //Defined checked Module
            $scope.checkedModule = {
                module: []
            };
        };

        //Hide alert message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");
        };

        //Get all roles
        $scope.getAllRoles = function () {
            //Define table as Datatable
            var table = $("#roleTable").DataTable();
            //Clear table
            table.clear();
            //Destroy table
            table.destroy();

            roleService.getRoles().then(function (result) {
                $scope.lstRoles = result.data;

                //Set table Configuration
                setTimeout(function () {
                    $("#roleTable").DataTable({
                        "aoColumnDefs": [{
                            "bSortable": false,
                            "aTargets": [-1]
                        }],
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": true,
                        "autoWidth": false
                    });
                }, 500);
            });
        };

        //Change flag for dispaly form
        $scope.addRole = function () {
            //Make Table flg false for display Add role form
            $scope.flgTable = false;
            //for Display Link
            $scope.UserState = "> Add Role";
        };

        //Edit role
        $scope.editRole = function (obj) {
            $scope.model.Id = obj.Id;
            $scope.model.Name = obj.Name;
            //For display role form
            $scope.flgTable = false;
            $scope.UserState = "> Edit Role";
        };

        //Create role
        $scope.createRole = function (obj) {
            $scope.hideMessage();
            roleService.insertUpdateRole(obj).then(function (result) {
                if (result.data.success === 1) {
                    //made message flag true for display message and add classes
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllRoles();
                    $scope.reset();
                }
                else {
                    //made message flag true for display message and add classes
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Delete role
        $scope.deleteRole = function (obj) {
            $scope.hideMessage();
            params = {
                id: obj.Id
            };
            roleService.deleteRole(obj.Id).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllRoles();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Reset model
        $scope.reset = function () {
            $scope.checkedModule.module = [];
            $scope.flgTable = true;
            $scope.UserState = "";
            //$scope.flgMessage = false;
            $scope.model = {
                ID: '',
                Name: '',
                UserPermission: []
            };
            $("#liTab_2").removeClass("active");
            $("#tab_2").removeClass("active");
            $("#liTab_1").addClass("active");
            $("#tab_1").addClass("active");
        };

        $scope.init();

    }]);

//Role Controller
app.controller('SettingCtrl', ['$scope', 'settingService',
    function ($scope, _settingService) {
        //Initialize data
        $scope.init = function () {

            $scope.model = {
                settingId: 0,
                name: '',
                value: ''
            };

            //for Display Setting list
            $scope.flgTable = true;

            //for Display Message
            $scope.flgMessage = false;

            //for message
            $scope.message = "";

            //for Setting Link
            $scope.UserState = "";

            $scope.getAllSettings();

        };

        //Hide alert message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");
        };

        //Get all settings
        $scope.getAllSettings = function () {
            //Define table as Datatable
            var table = $("#settingTable").DataTable();
            //Clear table
            table.clear();
            //Destroy table
            table.destroy();

            _settingService.getSettings().then(function (result) {
                $scope.lstSettings = result.data.result;

                //Set table Configuration
                setTimeout(function () {
                    $("#settingTable").DataTable({
                        "aoColumnDefs": [{
                            "bSortable": false,
                            "aTargets": [-1]
                        }],
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": true,
                        "autoWidth": false
                    });
                }, 500);
            });
        };

        //Change flag for display form
        $scope.addSetting = function () {
            //Make Table flg false for display Add setting form
            $scope.flgTable = false;
            //for Display Link
            $scope.UserState = "> Add Setting";
        };

        //Edit setting
        $scope.editSetting = function (obj) {
            $scope.model.settingId = obj.settingId;
            $scope.model.name = obj.name;
            $scope.model.value = obj.value;
            //For display role form
            $scope.flgTable = false;
            $scope.UserState = "> Edit Setting";
        };

        //Create/Edit Setting
        $scope.createEditSetting = function (obj) {
            $scope.hideMessage();
            console.log(obj);

            if (obj.settingId > 0) {
                _settingService.updateSetting(obj).then(function (result) {
                    if (result.data.success === 200) {
                        //made message flag true for display message and add classes
                        $scope.flgMessage = true;
                        $scope.message = "Setting has been updated successfully !";
                        $("#message").addClass("alert alert-success");
                        $("#icon").addClass("fa-thumbs-up");
                        $scope.getAllSettings();
                        $scope.reset();
                    }
                    else {
                        //made message flag true for display message and add classes
                        $scope.flgMessage = true;
                        $scope.message = "Setting could not be updated !";
                        $("#message").addClass("alert alert-danger");
                        $("#icon").addClass("fa-times");
                    }
                });
            }
            else {
                _settingService.insertSetting(obj).then(function (result) {
                    if (result.data.success === 200) {
                        //made message flag true for display message and add classes
                        $scope.flgMessage = true;
                        $scope.message = "Setting has been added successfully !";
                        $("#message").addClass("alert alert-success");
                        $("#icon").addClass("fa-thumbs-up");
                        $scope.getAllSettings();
                        $scope.reset();
                    }
                    else {
                        //made message flag true for display message and add classes
                        $scope.flgMessage = true;
                        $scope.message = "Setting could not be added !";
                        $("#message").addClass("alert alert-danger");
                        $("#icon").addClass("fa-times");
                    }
                });
            }
        };

        //Delete setting
        $scope.deleteSetting = function (obj) {
            $scope.hideMessage();

            _settingService.deleteSetting(obj.settingId).then(function (result) {
                if (result.data.result === 200) {
                    $scope.flgMessage = true;
                    $scope.message = "Setting has been deleted successfully!";
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllSettings();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = "Setting could not be deleted !";
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Reset model
        $scope.reset = function () {
            $scope.flgTable = true;
            $scope.UserState = "";
            //$scope.flgMessage = false;
            $scope.model = {
                settingId: 0,
                name: '',
                value: ''
            };
            $("#liTab_2").removeClass("active");
            $("#tab_2").removeClass("active");
            $("#liTab_1").addClass("active");
            $("#tab_1").addClass("active");
        };

        $scope.init();

    }]);

//Module Controller
app.controller('ModuleCtrl', ['$scope', 'moduleService',
    function ($scope, moduleService) {

        //Initialize data
        $scope.init = function () {
            $scope.model = {
                ID: '',
                DisplayOrder: '',
                ModuleName: '',
                PageIcon: '',
                PageUrl: '',
                PageSlug: '',
                IsDeleted: false,
                IsActive: true,
                IsDefault: false,
                ParentModuleID: 0
            };

            //for module link
            $scope.flgTable = true;
            //for display message
            $scope.flgMessage = false;
            //for message
            $scope.message = "";
            //for User link
            $scope.UserState = "";

            $scope.getAllModule();

        };

        //Hide alert message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");
        };

        //Get all module
        $scope.getAllModule = function () {
            //Define DataTable
            var table = $("#moduleTable").DataTable();
            table.clear();
            table.destroy();

            moduleService.getModuleList().then(function (result) {
                $scope.lstModule = result.data;
                $scope.lstModuleDropdown = result.data;

                //Set Table Configuration
                setTimeout(function () {
                    $('#moduleTable').DataTable({
                        "aoColumnDefs": [{
                            "bSortable": false,
                            "aTargets": [-1]
                        }],
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": true,
                        "autoWidth": false
                    });
                }, 500);
            });
        };

        //Open module form
        $scope.addModule = function () {
            //make table flg false for Display Add Module form
            $scope.flgTable = false;
            $scope.UserState = "> Add Module";
        };

        //Edit module data
        $scope.editModule = function (obj) {
            //Edit Module
            $scope.UserState = "> Edit Module";
            $scope.model.ID = obj.ID;
            $scope.model.DisplayOrder = obj.DisplayOrder;
            $scope.model.ModuleName = obj.ModuleName;
            $scope.model.PageIcon = obj.PageIcon;
            $scope.model.PageUrl = obj.PageUrl;
            $scope.model.PageSlug = obj.PageSlug;
            $scope.model.IsDeleted = obj.IsDeleted;
            $scope.model.IsActive = obj.IsActive;
            $scope.model.IsDefault = obj.IsDefault;
            $scope.model.ParentModuleID = obj.ParentModuleID;
            $scope.flgTable = false;
        };

        //Create/update module
        $scope.createModule = function (obj) {
            $scope.hideMessage();
            moduleService.insertUpdateModule(obj).then(function (result) {
                if (result.data.success === 1) {
                    //Make message flg true for display message
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllModule();
                    $scope.reset();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-exclamation-triangle");
                }
            });
        };

        //Delete Module
        $scope.deleteModule = function (obj) {
            $scope.hideMessage();
            params = {
                id: obj.ID
            };
            moduleService.deleteModule(params.id).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllModule();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Reset data
        $scope.reset = function () {
            $scope.flgTable = true;
            $scope.UserState = "";
            $scope.model = {
                ID: '',
                DisplayOrder: '',
                ModuleName: '',
                PageIcon: '',
                PageUrl: '',
                PageSlug: '',
                IsDeleted: false,
                IsActive: true,
                IsDefault: false,
                ParentModuleID: 0
            };
        };

        $scope.init();
    }]);

//Dashboard Controller
app.controller('DashboardCtrl', ['$scope', 'dashboardService',
    function ($scope, dashboardService) {

        //Initialize data
        $scope.init = function () {
            //Set monthlist for chart
            $scope.MonthList = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            $scope.list = [];
            $scope.DataList = [];

            $scope.model = {
                TotalUsers: 0,
                NewUsers: 0,
                UnconfirmedUsers: 0,
                BannedUsers: 0,
                UserMast: []
            };
            //Get Dashboard data
            dashboardService.dashboardData().then(function (result) {
                $scope.model.TotalUsers = result.data.TotalUsers;
                $scope.model.NewUsers = result.data.NewUsers;
                $scope.model.UnconfirmedUsers = result.data.UnconfirmedUsers;
                $scope.model.BannedUsers = result.data.BannedUsers;
                $scope.model.UserMast = result.data.UserMast;
                //Call Chart
                $scope.InitChart(result.data.UsersMapData);
            });
        };

        $scope.InitChart = function (UsersMapData) {
            //Make group by same month 
            var finalResult = MakeGroupList(UsersMapData);

            //Make Month and total list
            for (var i = 0; i < 12; i++) {
                var obj = new Object();
                obj.month = $scope.MonthList[i];
                obj.total = 0;
                $scope.list.push(obj);
            }

            //Update Month and total list
            for (var j = 0; j < $scope.list.length; j++) {
                for (k = 0; k < finalResult.length; k++) {
                    if ($scope.list[j].month === finalResult[k].month) {
                        $scope.list[j].total = finalResult[k].total;
                    }
                }
            }

            //Make final chart data list
            for (var l = 0; l < $scope.list.length; l++) {
                $scope.DataList.push($scope.list[l].total);
            }

            //Call chart
            $scope.CallChart($scope.MonthList, $scope.DataList);
        };

        $scope.CallChart = function (MonthList, DataList) {

            /* ChartJS
           * -------
           * Here we will create a few charts using ChartJS
           */

            //-----------------------
            //- MONTHLY USERS CHART -
            //-----------------------

            // Get context with jQuery - using jQuery's .get() method.
            var usersChartCanvas = $("#usersChart").get(0).getContext("2d");
            // This will get the first returned node in the jQuery collection.
            var usersChart = new Chart(usersChartCanvas);

            var usersChartData = {
                labels: MonthList,
                datasets: [
                    [],
                    {
                        label: "Users",
                        fillColor: "rgba(60,141,188,0.9)",
                        strokeColor: "rgba(60,141,188,0.8)",
                        pointColor: "#3b8bba",
                        pointStrokeColor: "rgba(60,141,188,1)",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "rgba(60,141,188,1)",
                        data: DataList
                    }
                ]
            };

            var usersChartOptions = {
                //Boolean - If we should show the scale at all
                showScale: true,
                //Boolean - Whether grid lines are shown across the chart
                scaleShowGridLines: true,
                //String - Colour of the grid lines
                scaleGridLineColor: "rgba(0,0,0,.05)",
                //Number - Width of the grid lines
                scaleGridLineWidth: 1,
                //Boolean - Whether to show horizontal lines (except X axis)
                scaleShowHorizontalLines: true,
                //Boolean - Whether to show vertical lines (except Y axis)
                scaleShowVerticalLines: true,
                //Boolean - Whether the line is curved between points
                bezierCurve: true,
                //Number - Tension of the bezier curve between points
                bezierCurveTension: 0.3,
                //Boolean - Whether to show a dot for each point
                pointDot: true,
                //Number - Radius of each point dot in pixels
                pointDotRadius: 4,
                //Number - Pixel width of point dot stroke
                pointDotStrokeWidth: 1,
                //Number - amount extra to add to the radius to cater for hit detection outside the drawn point
                pointHitDetectionRadius: 20,
                //Boolean - Whether to show a stroke for datasets
                datasetStroke: true,
                //Number - Pixel width of dataset stroke
                datasetStrokeWidth: 2,
                //Boolean - Whether to fill the dataset with a color
                datasetFill: true,
                //String - A legend template
                legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%=datasets[i].label%></li><%}%></ul>",
                //Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                maintainAspectRatio: true,
                //Boolean - whether to make the chart responsive to window resizing
                responsive: true
            };

            //Create the line chart
            usersChart.Line(usersChartData, usersChartOptions);

            //---------------------------
            //- END MONTHLY USERS CHART -
            //---------------------------
        };

        //make list group by
        function MakeGroupList(UsersMapData) {
            var withCategories = _.each(UsersMapData, function (elem) {
                elem.cat = moment(elem.InsertDate).format("MMM");
            });

            var groupedResult = _.groupBy(withCategories, 'cat');

            var finalResult = _.map(groupedResult, function (elems) {
                var valuesArray = _.pluck(elems, 'val'); // from group
                return {
                    month: elems[0] ? elems[0].cat : 0,
                    total: elems.length
                };
            });

            return finalResult;
        }

        $scope.init();
    }]);

//Forum Controller
app.controller('ForumCtrl', ['$scope', 'forumService',
    function ($scope, forumService) {

        //Initialize data
        $scope.init = function () {
            $scope.model = {
                ForumID: '',
                ForumName: '',
                Title: '',
                Description: '',
                CreatedTime: '',
                Status: '',
                CreatedBy: '',
                IsDefault: false
            };

            //for module link
            $scope.flgTable = true;
            //for display message
            $scope.flgMessage = false;
            //for message
            $scope.message = "";
            //for User link
            $scope.UserState = "";

            $scope.getAllForumList();
        };

        //Hide alert message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");
        };

        //Get all forum list
        $scope.getAllForumList = function () {
            //Define DataTable
            var table = $("#forumTable").DataTable();
            table.clear();
            table.destroy();

            forumService.getForumList().then(function (result) {
                $scope.lstForum = result.data;

                //Set Table Configuration
                setTimeout(function () {
                    $('#forumTable').DataTable({
                        "aoColumnDefs": [{
                            "bSortable": false,
                            "aTargets": [-1]
                        }],
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": true,
                        "autoWidth": false
                    });
                }, 500);
            });
        };

        //Open forum form
        $scope.addForum = function () {
            //make table flg false for Display Add Forum form
            $scope.flgTable = false;
            $scope.UserState = "> Add Forum";
        };

        //Edit forum data
        $scope.editForum = function (obj) {
            //Edit Module
            $scope.UserState = "> Edit Forum";
            $scope.model.ForumID = obj.ForumID;
            $scope.model.ForumName = obj.ForumName;
            $scope.model.Title = obj.Title;
            $scope.model.Description = obj.Description;
            $scope.model.CreatedTime = obj.CreatedTime;
            $scope.model.Status = obj.Status;
            $scope.model.CreatedBy = obj.CreatedBy;
            $scope.model.IsDefault = obj.IsDefault;
            $scope.flgTable = false;
        };

        //Create/update forum
        $scope.createForum = function (obj) {
            $scope.hideMessage();
            forumService.insertUpdateForum(obj).then(function (result) {
                if (result.data.success === 1) {
                    //Make message flg true for display message
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllForumList();
                    $scope.reset();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Delete Forum
        $scope.deleteForum = function (obj) {
            $scope.hideMessage();
            params = {
                id: obj.ForumID
            };

            forumService.deleteForum(params.id).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllForumList();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Reset data
        $scope.reset = function () {
            $scope.flgTable = true;
            $scope.UserState = "";

            $scope.model = {
                ForumID: '',
                ForumName: '',
                Title: '',
                Description: '',
                CreatedTime: '',
                Status: '',
                CreatedBy: '',
                IsDefault: false
            };
        };

        $scope.init();

    }]);

//ForumTopic Controller
app.controller('ForumTopicCtrl', ['$scope', 'forumTopicService', 'forumService',
    function ($scope, forumTopicService, forumService) {

        //Initialize data
        $scope.init = function () {
            $scope.model = {
                ForumTopicID: '',
                ForumTopicName: '',
                ForumTopicDescription: '',
                CreatedTime: '',
                CreatedBy: '',
                ForumID: '',
                ForumTopicTitle: '',
                ForumName: '',
                IsDefault: false
            };

            //for module link
            $scope.flgTable = true;
            //for display message
            $scope.flgMessage = false;
            //for message
            $scope.message = "";
            //for User link
            $scope.UserState = "";

            $scope.getAllForumTopicList();
            $scope.getAllForums();
        };

        //Hide alert message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");
        };

        //Get all forum topic list
        $scope.getAllForumTopicList = function () {
            //Define DataTable
            var table = $("#forumTopicTable").DataTable();
            table.clear();
            table.destroy();

            forumTopicService.getForumTopicList().then(function (result) {
                $scope.lstForumTopics = result.data;

                //Set Table Configuration
                setTimeout(function () {
                    $('#forumTopicTable').DataTable({
                        "aoColumnDefs": [{
                            "bSortable": false,
                            "aTargets": [-1]
                        }],
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": true,
                        "autoWidth": false
                    });
                }, 500);
            });
        };

        //Open forum topic form
        $scope.addForumTopic = function () {
            //make table flg false for Display Add Forum form
            $scope.flgTable = false;
            $scope.UserState = "> Add Forum Topic";
        };

        //Edit forum topic data
        $scope.editForumTopic = function (obj) {
            //Edit Module
            $scope.UserState = "> Edit ForumTopic";
            $scope.model.ForumTopicID = obj.ForumTopicID;
            $scope.model.ForumTopicName = obj.ForumTopicName;
            $scope.model.ForumTopicDescription = obj.ForumTopicDescription;
            $scope.model.CreatedTime = obj.CreatedTime;
            $scope.model.CreatedBy = obj.CreatedBy;
            $scope.model.ForumID = obj.ForumID;
            $scope.model.ForumTopicTitle = obj.ForumTopicTitle;
            $scope.model.ForumName = obj.ForumName;
            $scope.model.IsDefault = obj.IsDefault;
            $scope.flgTable = false;
        };

        //Create/update forum topic
        $scope.createForumTopic = function (obj) {
            $scope.hideMessage();
            forumTopicService.insertUpdateForumTopic(obj).then(function (result) {
                if (result.data.success === 1) {
                    //Make message flg true for display message
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllForumTopicList();
                    $scope.reset();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Delete Forum Topic
        $scope.deleteForumTopic = function (obj) {
            $scope.hideMessage();
            params = {
                id: obj.ForumTopicID
            };
            forumTopicService.deleteForumTopic(params.id).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllForumTopicList();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //get forum 
        $scope.getAllForums = function () {
            forumService.getForumList().then(function (result) {
                $scope.lstForums = result.data;
            });
        };

        //Reset data
        $scope.reset = function () {
            $scope.flgTable = true;
            $scope.UserState = "";

            $scope.model = {
                ForumTopicID: '',
                ForumTopicName: '',
                ForumTopicDescription: '',
                CreatedTime: '',
                CreatedBy: '',
                ForumID: '',
                ForumTopicTitle: '',
                ForumName: '',
                IsDefault: false
            };
        };

        $scope.init();
    }]);

//ForumPost Controller
app.controller('ForumPostCtrl', ['$scope', '$sce', 'forumTopicService', 'forumService', 'forumPostService',
    function ($scope, $sce, forumTopicService, forumService, forumPostService) {

        //Initialize data
        $scope.init = function () {
            $scope.model = {
                ForumPostID: 0,
                ForumPostTitle: '',
                ForumPostContent: '',
                CreatedTime: null,
                CreatedBy: '',
                ForumTopicID: 0,
                ForumID: 0,
                ShowInPortal: false,
                PostLocked: false,
                ReviewCount: 0,
                CreatedByUserName: '',
                ForumName: '',
                ForumTopicName: '',
                IsDefault: false

            };

            //for module link
            $scope.flgTable = true;
            //for display message
            $scope.flgMessage = false;
            //for message
            $scope.message = "";
            //for User link
            $scope.UserState = "";

            //tinymce plugin and toolbar options
            $scope.tinymceOptions = {
                selector: '#post',
                menubar: false,
                plugins: [
                    "advlist autolink autoresize link image fullscreen  lists charmap paste print preview hr anchor pagebreak",
                    "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking wordcount  ",
                    "table contextmenu directionality emoticons paste template spellchecker searchreplace responsivefilemanager help imagetools media youtube "
                ],
                mobile: {
                    theme: "mobile",
                    plugins: ['autosave', 'lists', 'autolink'],
                    toolbar: ['undo', 'bold', 'italic', 'styleselect']
                },
                toolbar_items_size: 'small',
                toolbar: " undo redo | bold italic underline | aligncenter alignright alignjustify | forecolor  backcolor | fontselect | fontsizeselect | link | responsivefilemanager youtube | emoticons | searchreplace | help preview ",
                image_dimensions: false,
                image_class_list: [
                    { title: 'Responsive', value: 'img-responsive' }
                ],

                link_context_toolbar: true,
                image_advtab: true,
                paste_data_images: true,

                external_filemanager_path: "/filemanager/",
                filemanager_title: "Responsive Filemanager",
                external_plugins: { "filemanager": "/filemanager/plugin.min.js" },

                width: "98%",
                emoticons_database_url: '/Content/blackfyre/js/SpecialTinymce/js/tinymce/plugins/emoticons/js/emojis.js',
                entity_encoding: "numeric"

            };

            $scope.getAllForumPostList();
            $scope.getAllForums();
            $scope.showForumTopics();
            $scope.lstTopic = null;
        };

        //Hide alert message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");
        };

        //get forum 
        $scope.getAllForums = function () {
            forumService.getForumList().then(function (result) {
                $scope.lstForums = result.data;
            });
        };

        //get forumtopic
        $scope.showForumTopics = function () {
            $scope.lstTopic = null;
            forumTopicService.getForumTopicList().then(function (result) {
                $scope.lstForumTopics = result.data;
            });
        };

        //Get all forum post list
        $scope.getAllForumPostList = function () {
            //Define DataTable
            var table = $("#forumPostTable").DataTable();
            table.clear();
            table.destroy();

            forumPostService.getForumPostList().then(function (result) {
                $scope.lstForumPosts = result.data;

                //Set Table Configuration
                setTimeout(function () {
                    $('#forumPostTable').DataTable({
                        "aoColumnDefs": [{
                            "bSortable": false,
                            "aTargets": [-1]
                        }],
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": true,
                        "autoWidth": false
                    });
                }, 500);
            });
        };

        //Get all forum topic list wtih forumID
        $scope.getAllForumTopicByID = function (obj) {
            $scope.lstForumTopics = null;
            params = {
                id: obj
            };
            $scope.lstForumTopics = null;
            forumTopicService.getForumTopicListGetByForumID({ params: params }).then(function (result) {
                $scope.lstTopic = result.data;
            });
        };

        //Open forum post form
        $scope.addForumPost = function () {
            //make table flg false for Display Add Forum Post form
            $scope.flgTable = false;
            $scope.UserState = "> Add Forum Post";
        };

        //Edit forum post data
        $scope.editForumPost = function (obj) {
            //Edit post
            $scope.UserState = "> Edit ForumPost";
            $scope.model.ForumPostID = obj.ForumPostID;
            $scope.model.ForumPostTitle = obj.ForumPostTitle;
            $scope.model.ForumPostContent = obj.ForumPostContent;
            $scope.model.CreatedTime = obj.CreatedTime;
            $scope.model.CreatedBy = obj.CreatedBy;
            $scope.model.ForumTopicID = obj.ForumTopicID;
            $scope.model.ForumID = obj.ForumID;
            $scope.model.ShowInPortal = obj.ShowInPortal;
            $scope.model.PostLocked = obj.PostLocked;
            $scope.model.ReviewCount = obj.ReviewCount;
            $scope.model.CreatedByUserName = obj.CreatedByUserName;
            $scope.model.ForumName = obj.ForumName;
            $scope.model.ForumTopicName = obj.ForumTopicName;
            $scope.model.IsDefault = obj.IsDefault;
            $scope.flgTable = false;

            //var sample = $scope.model.ForumPostContent;
            //sample = sample.replace(/\\U([0-9a-f]{8})/gi, "&#x$1;");
            //document.getElementById('demo').innerHTML = sample;
            $scope.renderHtml = function (html) {
                return $sce.trustAsHtml(html);
            };

        };

        //Create/update forum post
        $scope.createForumPost = function (obj) {
            $scope.hideMessage();
            forumPostService.insertUpdateForumPost(obj).then(function (result) {
                if (result.data.success === 1) {
                    //Make message flg true for display message
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllForumPostList();
                    $scope.reset();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Delete Forum Post
        $scope.deleteForumPost = function (obj) {
            $scope.hideMessage();
            params = {
                id: obj.ForumPostID
            };
            forumPostService.deleteForumPost(params.id).then(function (result) {
                if (result.data.success === 1) {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.getAllForumPostList();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Reset data
        $scope.reset = function () {
            $scope.lstTopic = null;
            $scope.flgTable = true;
            $scope.UserState = "";

            $scope.model = {
                ForumPostID: 0,
                ForumPostTitle: '',
                ForumPostContent: '',
                CreatedTime: null,
                CreatedBy: '',
                ForumTopicID: 0,
                ForumID: 0,
                ShowInPortal: false,
                PostLocked: false,
                ReviewCount: 0,
                CreatedByUserName: '',
                ForumName: '',
                ForumTopicName: '',
                IsDefault: false
            };
            $scope.tinymceOptions2 = null;
        };

        $scope.init();
    }]);

//ChangePassword Controller
app.controller('ChangePassCtrl', ['$scope', 'userService',
    function ($scope, userService) {

        $scope.init = function () {

            $scope.model = {
                OldPass: '',
                NewPass: '',
                CheckPass: '',
            };
            //for display message
            $scope.flgMessage = false;
            //for message
            $scope.message = "";
        };

        //Hide alert message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");
        };

        //update the password
        $scope.changePass = function (obj) {
            $scope.hideMessage();
            userService.changePassword(obj).then(function (result) {
                if (result.data.success === 1) {
                    //Make message flg true for display message
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-success");
                    $("#icon").addClass("fa-thumbs-up");
                    $scope.reset();
                }
                else {
                    $scope.flgMessage = true;
                    $scope.message = result.data.message;
                    $("#message").addClass("alert alert-danger");
                    $("#icon").addClass("fa-times");
                }
            });
        };

        //Reset data
        $scope.reset = function () {
            $scope.model = {
                OldPass: '',
                NewPass: '',
                CheckPass: '',
            };
        };

        $scope.init();
    }]);



/*
These controllers that are in the below belongs to the Normal Page like Forum,ForumPost, TopicDetail
*/

//Portal Controller
app.controller('PortalCtrl', ['$scope', '$http', '$sce', '$sanitize', 'portalMessageService',
    function ($scope, $http, $sce, $sanitize, portalMessageService) {

        $scope.userName = null;
        $scope.generalHub = null; // holds the reference to hub

        $scope.generalHub = $.connection.generalHub; // initializes hub
        $.connection.hub.start(); // starts hub

        // register a client method on hub to be invoked by the server
        $scope.generalHub.client.addNewMessage = function (name, message) {
            $scope.playSound();
            $scope.getAllPortalMessages();
            $scope.$apply();
        };

        $scope.createMessage = function (obj) {
            $scope.sanitizedText = $sanitize($scope.textMessage);
            $scope.userName = obj;
            $scope.textMessage = null;
            $("#text-message").data("emojioneArea").setText('');  // Reset Emoji field
            $scope.generalHub.server.send($scope.userName, $scope.sanitizedText);

        };

        $scope.init = function () {
            $scope.textMessage = null;
            $scope.model = {
                MessageID: 0,
                PortalMessageUserID: '',
                TextMessage: '',
                SendDate: null,
                UserID: '',
                UserName: '',
                PhotoPath: '',
                TagUserName: '',
                TagColor: ''
            };
            //for display message
            $scope.flgMessage = false;
            //for message
            $scope.message = "";
            $scope.getAllPortalMessages();

            $scope.getOnlineUsers();

            //emoji input text settings
            $("#text-message").emojioneArea({
                pickerPosition: "bottom",
                filtersPosition: "bottom",
                saveEmojisAs: "unicode"
            });
        };

        $scope.slimScroll = function () {
            setTimeout(function () {
                $("#chat-box").slimScroll({
                    start: 'bottom',
                });
            }, 200);
        };

        //Hide alert message
        $scope.hideMessage = function () {
            //make message flg false for hide message
            $scope.flgMessage = false;
            $("#message").removeClass("alert alert-success").removeClass("alert alert-danger");
            $("#icon").removeClass("fa-check").removeClass("fa-ban");
        };

        $scope.getAllPortalMessages = function () {
            portalMessageService.getMessages().then(function (result) {
                $scope.lstMessages = result.data.result;
                $scope.slimScroll();
            });
        };

        $scope.playSound = function () {
            var media = document.getElementById("play");
            media.pause();
            const playPromise = media.play();
            if (playPromise !== null) {
                playPromise.catch(() => { media.play(); })
            }
        };

        //if the user presses enter button from a keyboard
        $scope.keyDown = function (userName) {
            $scope.textMessage = $("#text-message").data("emojioneArea").getText(); //getting text       
            $("#text-message").data("emojioneArea").setText('');  // Reset Emoji field
            $scope.createMessage(userName);
        };

        $scope.getOnlineUsers = function () {
            $scope.onlineUsersList = $scope.onlineUsers;
        };

        $scope.generalHub.client.online = function (list) {
            $scope.onlineUsers = list;
        };

        $scope.deleteMessage = function (obj) {
            params = {
                id: obj
            };
            portalMessageService.deleteMessage(params.id).then(function (result) {
                $scope.getAllPortalMessages();
            });
        };

        $scope.renderHtml = function (html) {
            return $sce.trustAsHtml(html);
        };

        //Reset data
        $scope.reset = function () {
            $scope.model = {
                MessageID: 0,
                PortalMessageUserID: '',
                TextMessage: '',
                SendDate: null,
                UserID: '',
                UserName: '',
                PhotoPath: '',
                TagUserName: '',
                TagColor: ''
            };
        };

        $scope.init();

    }]);

//Messenger Controller
app.controller('MessengerCtrl', ['$scope', '$http', '$sce', '$sanitize', 'chatService', 'chatboxService',
    function ($scope, $http, $sce, $sanitize, chatService, chatboxService) {

        $scope.generalHub = null; // holds the reference to hub
        $scope.generalHub = $.connection.generalHub; // initializes hub
        $.connection.hub.start(); // starts hub

        $scope.init = function () {

            $scope.model = {
            };

            $scope.textMessage = null;
            //for display message
            $scope.flgMessage = false;
            //for message
            $scope.message = "";
            $scope.showPlus = true;
            $scope.showQuery = false;
            $scope.showMessageBox = false;
            $scope.trashBox = false;
            $scope.emptyBox = true;
            $scope.selectedBoxCount = null;
            $scope.SenderName = '';
            $scope.gettingChatboxID = '';
            $scope.ReceiverName = '';
            $scope.getUserList(); // to get a real username search
            $scope.getReceiverChatbox();
            $scope.getSenderChatbox();

            //defined checked chatBox to be deleted
            $scope.checkedBox = {
                chatBox: [],
            };

            $("#text-message").emojioneArea({
                pickerPosition: "top",
                filtersPosition: "bottom",
                searchPosition: "bottom",
                saveEmojisAs: "unicode"
            });
        };

        // register a client method on hub to be invoked by the server
        $scope.generalHub.client.newMessage = function (boxID) {
            $scope.playSound();
            $scope.getBox(boxID);
            $scope.slimScroll();
            $scope.refreshBoxes();
            $scope.$apply();
        };

        $scope.getReceiverChatbox = function () {
            var userID = $('input[id="getuserID"]').val();
            params = {
                id: userID
            };

            chatboxService.getBoxListByID({ params: params }).then(function (result) {
                $scope.lstReceiverList = result.data.ChatBoxes_ReceiverID;
            })
        };

        $scope.getSenderChatbox = function () {
            var userID = $('input[id="getuserID"]').val();
            params = {
                id: userID
            };

            chatboxService.getBoxListByID({ params: params }).then(function (result) {
                $scope.lstSenderList = result.data.ChatBoxes_SenderID;
            })
        };

        $scope.setQuery = function (query) {
            $scope.query = query;
            $scope.focus = false;
        };

        $scope.getUserList = function () {
            chatboxService.getUserNameList().then(function (result) {
                $scope.userList = result.data;
            })
        };

        $scope.getBox = function (obj) {
            $scope.gettingChatboxID = obj;
            //getting by chatboxID
            params = {
                id: obj
            };
            //getting chats
            chatService.getChatsByBoxID({ params: params }).then(function (result) {
                $scope.lstChatList = result.data.Sender;
                $scope.slimScroll();
            });
        };

        $scope.showMessenger = function (obj) {
            $scope.showMessageBox = true;
            $scope.emptyBox = false;
            $scope.getBox(obj);
        };

        $scope.sendMessage = function () {
            // sends a new message to the server
            var chatBoxs = $('input[id="ChatBoxs"]').val();
            var userID = $('input[id="getuserID"]').val();
            var receiverName = $('input[id="getReceiver"]').val();
            var receiverName2 = $('input[id="getReceiver2"]').val();

            $scope.sanitizedText = $sanitize($scope.textMessage);

            if (chatBoxs === undefined) {  //if there is no any chat message, we have to set up chatBoxs value to send a message
                chatBoxs = $scope.gettingChatboxID;
            }

            data = {
                id: userID,
                chbx: chatBoxs,
                text: $scope.sanitizedText,
                receiver: receiverName,
                receiver2: receiverName2
            }

            $("#text-message").data("emojioneArea").setText('');  // Reset Emoji field
            $scope.textMessage = null;
            $scope.generalHub.server.message(data.chbx, data.text, data.id, data.receiver, data.receiver2); //send a real time message
            $scope.generalHub.server.notificationForMessage(data.receiver, data.receiver2); //send a real time notification
        };

        $scope.showSearch = function () {
            $scope.showPlus = false;
            $scope.showQuery = true;
        };

        $scope.playSound = function () {
            var media = document.getElementById("play");
            media.pause();
            const playPromise = media.play();
            if (playPromise !== null) {
                playPromise.catch(() => { media.play(); })
            }
        };

        $scope.selectBox = function () {
            $scope.trashBox = true;
        };

        $scope.slimScroll = function () {
            setTimeout(function () {
                $("#chat-box").slimScroll({
                    start: 'bottom',
                });
            }, 200);
        };

        $scope.refreshBoxes = function () {
            setTimeout(function () {
                $scope.getReceiverChatbox();
                $scope.getSenderChatbox();
            }, 200);
        };

        $scope.createBox = function () {
            params = {
                username: $scope.query
            };
            chatboxService.insertBox(params.username).then(function (result) {
                $scope.getReceiverChatbox();
            })
        };

        $scope.deleteChatboxes = function () {
            //getting checked boxes
            params = {
                boxID: $scope.checkedBox.chatBox
            };
            chatboxService.deleteBox({ params: params }).then(function (result) {
                $scope.reset();
            });

        };

        $scope.deleteMessage = function (obj, boxID) {
            params = {
                id: obj
            };
            chatService.deleteChatByID(params.id).then(function (result) {
                $scope.getBox(boxID);
            });
        };

        $scope.closeMessageBox = function () {
            $scope.showMessageBox = false;
            $scope.emptyBox = true;
        };

        $scope.closeQueryBox = function () {
            $scope.showQuery = false;
            $scope.showPlus = true;
        };

        $scope.renderHtml = function (html) {
            return $sce.trustAsHtml(html);
        };

        $scope.reset = function () {

            $scope.model = {
            };

            //for display message
            $scope.flgMessage = false;
            //for message
            $scope.message = "";
            $scope.showPlus = true;
            $scope.showQuery = false;
            $scope.showMessageBox = false;
            $scope.trashBox = false;
            $scope.emptyBox = true;
            $scope.selectedBoxCount = null;
            $scope.SenderName = '';
            $scope.gettingChatboxID = '';
            $scope.ReceiverName = '';
            $scope.getUserList(); // to get a real username search
            $scope.getReceiverChatbox();
            $scope.getSenderChatbox();

            //defined checked chatBox to be deleted
            $scope.checkedBox = {
                chatBox: [],
            };
        };

        //if the user presses enter button from a keyboard
        $scope.keyDown = function () {
            $scope.textMessage = $("#text-message").data("emojioneArea").getText(); //getting text       
            $("#text-message").data("emojioneArea").setText('');  // Reset Emoji field
            $scope.sendMessage();
        };

        $scope.init();
    }]);

//TopicDetail Controller
app.controller('DetailCtrl', ['$scope', '$sce', '$window', 'forumPostService', 'forumPostCommentService',
    function ($scope, $sce, $window, forumPostService, forumPostCommentService) {

        $scope.generalHub = null; // holds the reference to hub

        $scope.generalHub = $.connection.generalHub; // initializes hub
        $.connection.hub.start(); // starts hub

        $scope.init = function () {
            $scope.model = {

            };
            $scope.getDetail(); //getting a detail of a forum post
            $scope.getAllComments(); //getting all comments of a forum post
            $scope.commentID = null;
            $scope.commentText = null;
            $scope.postDetailShow = true;
            $scope.editPostShow = false;

            //tinymce plugin and toolbar options
            $scope.tinymceOptions = {
                selector: '#post',
                menubar: false,
                skin: "oxide-dark",
                plugins: [
                    "advlist autolink autoresize link image fullscreen  lists charmap paste print preview hr anchor pagebreak",
                    "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking wordcount  ",
                    "table contextmenu directionality emoticons paste template spellchecker searchreplace responsivefilemanager help imagetools media youtube "
                ],
                mobile: {
                    theme: "mobile",
                    plugins: ['autosave', 'lists', 'autolink'],
                    toolbar: ['undo', 'bold', 'italic', 'styleselect']
                },
                toolbar_items_size: 'small',
                toolbar: " undo redo | bold italic underline | aligncenter alignright alignjustify | forecolor  backcolor | fontselect | fontsizeselect | link | responsivefilemanager youtube | emoticons | searchreplace | help preview ",
                image_dimensions: false,
                image_class_list: [
                    { title: 'Responsive', value: 'img-responsive' }
                ],

                link_context_toolbar: true,
                image_advtab: true,
                paste_data_images: true,

                external_filemanager_path: "/filemanager/",
                filemanager_title: "Responsive Filemanager",
                external_plugins: { "filemanager": "/filemanager/plugin.min.js" },

                width: "98%",
                emoticons_database_url: '/Content/blackfyre/js/SpecialTinymce/js/tinymce/plugins/emoticons/js/emojis.js',
                entity_encoding: "numeric"
            };

            //tinymce plugin and toolbar options
            $scope.tinymceforEdit = {
                selector: '#detailEdit',
                menubar: false,
                skin: "oxide-dark",
                plugins: [
                    "advlist autolink autoresize link image fullscreen  lists charmap paste print preview hr anchor pagebreak",
                    "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking wordcount  ",
                    "table contextmenu directionality emoticons paste template spellchecker searchreplace responsivefilemanager help imagetools media youtube "
                ],
                mobile: {
                    theme: "mobile",
                    plugins: ['autosave', 'lists', 'autolink'],
                    toolbar: ['undo', 'bold', 'italic', 'styleselect']
                },
                toolbar_items_size: 'small',
                toolbar: " undo redo | bold italic underline | aligncenter alignright alignjustify | forecolor  backcolor | fontselect | fontsizeselect | link | responsivefilemanager youtube | emoticons | searchreplace | help preview ",
                image_dimensions: false,
                image_class_list: [
                    { title: 'Responsive', value: 'img-responsive' }
                ],

                link_context_toolbar: true,
                image_advtab: true,
                paste_data_images: true,

                external_filemanager_path: "/filemanager/",
                filemanager_title: "Responsive Filemanager",
                external_plugins: { "filemanager": "/filemanager/plugin.min.js" },

                width: "98%",
                emoticons_database_url: '/Content/blackfyre/js/SpecialTinymce/js/tinymce/plugins/emoticons/js/emojis.js',
                entity_encoding: "numeric"
            };
        };

        $scope.getDetail = function () {
            var postID = $('input[id="getPostID"]').val();
            forumPostService.getTopicDetail(postID).then(function (result) {
                $scope.postDetail = result.data.result;
            })
        };

        $scope.getAllComments = function () {
            var getpostID = $('input[id="getPostID"]').val();
            forumPostCommentService.getCommentListByPostID(getpostID).then(function (result) {
                $scope.postComments = result.data.result;
            })
        };

        $scope.renderHtml = function (html) {
            return $sce.trustAsHtml(html);
        };

        //create/update comment
        $scope.createComment = function (obj, userName) {

            if ($scope.commentID === null) {
                //obj is forumPost
                params = {
                    PostID: obj,
                    Text: $scope.commentText
                };

                forumPostCommentService.insertUpdateComment(params).then(function (result) {
                    $scope.generalHub.server.notificationForComment(obj, userName);
                    $scope.getAllComments();
                    tinymce.activeEditor.setContent("");
                });

            }
            else {
                //obj is forumPost
                params = {
                    CommentID: $scope.commentID,
                    PostID: obj,
                    Text: $scope.commentText
                };

                forumPostCommentService.updateComment(params).then(function (result) {
                    $scope.getAllComments();
                    $scope.commentID = null;
                    tinymce.activeEditor.setContent("");
                });
            }

        };

        $scope.editPost = function (obj) {

            $scope.postDetailShow = false;
            $scope.editPostShow = true;
            $scope.model.forumPostID = $scope.postDetail.forumPostID;
            $scope.model.forumPostTitle = $scope.postDetail.forumPostTitle;
            $scope.model.forumPostContent = $scope.postDetail.forumPostContent;
        };

        $scope.deleteComment = function (obj) {
            params = {
                id: obj
            };
            forumPostCommentService.deleteComment(params.id).then(function (result) {
                $scope.getAllComments();
            });

        };

        $scope.editComment = function (obj) {
            $scope.commentID = obj;
            forumPostCommentService.getEditComment(obj).then(function (result) {
                $scope.commentText = result.data.result.text;
            })
        };

        $scope.reset = function () {
            $scope.init();
        };

        $scope.updatePost = function (obj) {
            //obj is postId
            params = {
                PostID: obj,
                Content: $scope.model.forumPostContent,
                Title: $scope.model.forumPostTitle
            };
            forumPostService.updatePostDetail(params).then(function (result) {
                if (result.data.result === 200) {
                    $scope.init();
                }
            });
        };

        $scope.deletePost = function (obj) {

            forumPostService.deletePost(obj).then(function (result) {
                if (result.data.result === 200) {
                    $window.location.href = '/Home/Index';
                }
            });

        };

        $scope.init();

    }]);

app.controller('LayoutCtrl', ['$scope', '$window', 'notificationService',
    function ($scope, $window, notificationService) {

        $scope.userName = null;
        $scope.generalHub = null; // holds the reference to hub

        $scope.generalHub = $.connection.generalHub; // initializes hub
        $.connection.hub.start(); // starts hub

        $scope.init = function () {

            var IsLoggedUser = $('input[id="IsLogged"]').val();
            //for general notifications
            $scope.notifInit = true;
            $scope.TotalShow = false;

            //for message notifications
            $scope.notifInitMssg = true;
            $scope.showMssgNotif = false;

            $scope.settings = false;

            if (IsLoggedUser == 'True') {
                //$scope.getTotalNotif(); //for general notifications
                //$scope.getTotalMessageNotif(); //for message notifications
            }
        };

        // for general notifications
        $scope.generalHub.client.newNotification = function (obj) {
            $scope.notifInit = false;
            $scope.TotalShow = true;
            $scope.totalNotf = obj;

            $scope.getTotalNotif();
            $scope.playSound();
            $scope.$apply();
        };

        // for message notifications
        $scope.generalHub.client.newMessageNotif = function (obj) {
            $scope.notifInitMssg = false;
            $scope.showMssgNotif = true;
            $scope.totalMssgNotf = obj;

            $scope.getTotalMessageNotif();
            $scope.playSound();
            $scope.$apply();
        };

        //for general notificatiomns
        $scope.getTotalNotif = function () {
            notificationService.getGeneralNotifCount().then(function (result) {
                $scope.totalNotf = result.data.TotalNotification;
            });

            notificationService.getGeneralNotif().then(function (result) {
                $scope.NotfList = result.data;
                $scope.slimScroll();
            });

        };

        //for message notifications
        $scope.getTotalMessageNotif = function () {

            notificationService.getMessageCountNotif().then(function (result) {
                $scope.totalMssgNotf = result.data.TotalNotification;
            });

            notificationService.getMessageNotif().then(function (result) {
                $scope.messageNotifList = result.data;
                $scope.slimScroll();
            });

        };

        $scope.playSound = function () {
            var media = document.getElementById("play");
            media.pause();
            const playPromise = media.play();
            if (playPromise !== null) {
                playPromise.catch(() => { media.play(); })
            }
        };

        $scope.showSettings = function () {
            document.getElementById("dropDownList").classList.toggle("MenuShow");
        };

        $scope.showNotifications = function () {
            document.getElementById("MenuBar").classList.toggle("MenuShow");
        };

        $scope.slimScroll = function () {
            setTimeout(function () {
                $("#messageBox").slimScroll({
                    start: 'bottom',
                    height: '150px'
                });
            }, 200);
        };

        $scope.showMessages = function () {
            document.getElementById("messageBar").classList.toggle("MenuShow");
        };

        // Close the dropdown if the user clicks outside of it
        $window.onclick = function (e) {
            if (!e.target.matches('#elUserLink')) {
                var myDropdown = document.getElementById("dropDownList");
                if (myDropdown.classList.contains('MenuShow')) {
                    myDropdown.classList.remove('MenuShow');
                }
            }

        }

        $scope.init();

    }]);



