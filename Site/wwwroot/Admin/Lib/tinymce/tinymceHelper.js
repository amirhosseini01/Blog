function initTinymce() {
    tinymce.init({
        selector: 'textarea#VmInput_Description',
        plugins: 'codesample code link autolink fullscreen autosave directionality emoticons lists pagebreak nonbreaking insertdatetime preview searchreplace table visualblocks visualchars wordcount image',
        codesample_languages: [
            { text: 'HTML/XML', value: 'markup' },
            { text: 'JavaScript', value: 'javascript' },
            { text: 'CSS', value: 'css' },
            { text: 'PHP', value: 'php' },
            { text: 'Ruby', value: 'ruby' },
            { text: 'Python', value: 'python' },
            { text: 'Java', value: 'java' },
            { text: 'C', value: 'c' },
            { text: 'C#', value: 'csharp' },
            { text: 'C++', value: 'cpp' }
        ],
        toolbar: 'codesample code ltr rtl preview searchreplace wordcount image',
        image_title: true,
        images_upload_handler: image_upload_handler,
        directionality : 'rtl',
        language: 'fa_IR',
        language_url: '/Admin/lib/tinymce/fa_IR.js',
        // paste_data_images: false,
        // paste_block_drop: false,
        // paste_as_text: true
    });
}

const image_upload_handler = (blobInfo, progress) => new Promise((resolve, reject) => {
    const xhr = new XMLHttpRequest();
    xhr.withCredentials = false;
    xhr.open('POST', '/Admin/Index?handler=UploadImage');
    xhr.setRequestHeader('XSRF-TOKEN', $('input:hidden[name="__RequestVerificationToken"]').val());
  
    xhr.upload.onprogress = (e) => {
      progress(e.loaded / e.total * 100);
    };
  
    xhr.onload = () => {
      if (xhr.status === 403) {
        reject({ message: 'HTTP Error: ' + xhr.status, remove: true });
        return;
      }
  
      if (xhr.status < 200 || xhr.status >= 300) {
        reject('HTTP Error: ' + xhr.status);
        return;
      }
  
      const json = JSON.parse(xhr.responseText);
      if (!json || typeof json.Obj != 'string') {
        reject('Invalid JSON: ' + xhr.responseText);
        return;
      }
  
      resolve(json.Obj);
    };
  
    xhr.onerror = () => {
      reject('Image upload failed due to a XHR Transport error. Code: ' + xhr.status);
    };
  
    const formData = new FormData();
    formData.append('file', blobInfo.blob(), blobInfo.filename());
  
    xhr.send(formData);
  });