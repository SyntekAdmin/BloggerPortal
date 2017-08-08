
        var jqXHRData;

        $(document).ready(function () {
            initSimpleFileUpload();
            initSimpleFileUploadProfile();
        });

        function initSimpleFileUploadProfile() {
            'use strict';

            //{"isUploaded":true,"message":"File uploaded successfully!"}

            $('#characteruploadFileProFile').fileupload({
                url: '/uploadfileprofile',
                dataType: 'json',
                add: function (e, data) {
                    jqXHRData = data
                },
                done: function (event, data) {
                    if (data.result.isUploaded) {
                        fnSelectPhotoUrl(data.result.message);
                    }
                    else {
                        alert(data.result.message)
                    }
                    //alert(data.result.message);
                },
                fail: function (event, data) {
                    if (data.files[0].error) {
                        alert(data.files[0].error);
                    }
                }
            });
        }

        function initSimpleFileUpload() {
            'use strict';

            //{"isUploaded":true,"message":"File uploaded successfully!"}

            $('#characteruploadFile').fileupload({
                url: '/uploadfile',
                dataType: 'json',
                add: function (e, data) {
                    jqXHRData = data
                },
                done: function (event, data) {
                    if (data.result.isUploaded) {
                        fnSelectPhotoUrl(data.result.message);
                    }
                    else {
                        alert(data.result.message)
                    }
                    //alert(data.result.message);
                },
                fail: function (event, data) {
                    if (data.files[0].error) {
                        alert(data.files[0].error);
                    }
                }
            });
        }

        function chk_file_type(obj) {
            var file_kind = obj.value.lastIndexOf('.');
            var file_name = obj.value.substring(file_kind+1,obj.length);
            var file_type = file_name.toLowerCase();

            if (file_type == "jpg" || file_type == "jpeg" || file_type == "png" || file_type == "gif")
            {
                $(document).ready(function () {
                    if (jqXHRData) {
                        jqXHRData.submit();
                    }
                    return false;
                });
            }
            else
            {
                alert('select image file');
                return false;
            }
        }

        function fnSelectPhotoUrl(url) {
            $(document).ready(function () {
                SaveProfilePhoto(url);
                //
                $('#photoUrl').val(url);
                $('#userPhotoHead').attr("src", url);
                $('#userPhoto').attr("src", url);
                $('#userPhotoMo').attr("src", url);
                $("body, .r_container, .desktop_overlay").removeClass("open");
            });
        };

        function openDialog() {
            $('#characteruploadFile').click();
        };
        function openDialogProFile() {
            $('#characteruploadFileProFile').click();
        };
      
