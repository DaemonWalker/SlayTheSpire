var app = new Vue({
    el: "#app",
    data: {
        saveData: '',
        saveBak: '',
        saveStr: '',
        saveFileName: ''
    },
    methods: {
        convertSave: async function () {
            this.postData("/api/cheater/upload", '"' + this.saveStr + '"', null,
                res => {
                    this.saveData = JSON.parse(res.data);
                    this.saveBak = JSON.parse(res.data);
                    console.log(this.saveData.gold);
                });
        },
        exportSave: async function () {
            this.postData("/api/cheater/export", {
                "new": JSON.stringify(this.saveData),
                "bak": JSON.stringify(this.saveBak),
                "saveName": this.saveFileName
            }, "blob", res => {
                //let blob = new Blob([res.data], { type: "application/x-zip-compressed" });
                //let objectUrl = URL.createObjectURL(blob);
                //let link = document.createElement('a');
                //link.style.display = 'none';
                //link.href = objectUrl;
                //link.setAttribute('download', 'excel.xlsx');

                //document.body.appendChild(link);
                //link.click();

                //window.location.href = objectUrl;
                let headers = res.headers;
                let contentType = headers['content-type'];
                const blob = new Blob([res.data], { type: contentType });
                const contentDisposition = res.headers['content-disposition'];
                let fileName = 'unknown';
                if (contentDisposition) {
                    fileName = window.decodeURI(res.headers['content-disposition'].split(';')[1].split('=')[1]);
                }
                this.downFile(blob, fileName);
            });
        },
        downFile: function (blob, fileName) {
            // 非IE下载
            if ('download' in document.createElement('a')) {
                let link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob); // 创建下载的链接
                link.download = fileName; // 下载后文件名
                link.style.display = 'none';
                document.body.appendChild(link);
                link.click(); // 点击下载
                window.URL.revokeObjectURL(link.href); // 释放掉blob对象
                document.body.removeChild(link); // 下载完成移除元素
            } else {
                // IE10+下载
                window.navigator.msSaveBlob(blob, fileName);
            }
        },
        postData: async function (apiUrl, data, responseType, then) {
            var url = await getSaveServiceUrl();
            if (responseType === null) {
                axios({
                    method: 'post',
                    url: url + apiUrl,
                    data: data,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(then).catch(function (error) {
                    console.log(error);
                });
            } else {
                axios({
                    method: 'post',
                    url: url + apiUrl,
                    data: data,
                    responseType: responseType,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(then).catch(function (error) {
                    console.log(error);
                });
            }
        }
    }
});