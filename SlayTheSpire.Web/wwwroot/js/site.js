// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
const consulUrl = "api/consul/getsaveservice";
async function getSaveServiceUrl() {
    try {
        const { data: url } = await axios.get(consulUrl);
        return url;
    } catch (error) {
        console.log(error);
    }
}