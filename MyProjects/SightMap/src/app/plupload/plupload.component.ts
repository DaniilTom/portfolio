import { Component, ViewChild, ElementRef, Input } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/timer';
import 'rxjs/add/operator/take';
import { Subject } from 'rxjs';


declare let plupload: any;


@Component({
  selector: 'plupload-comp',
  templateUrl: './plupload.component.html'
})
export class PluploadComponent {

  

  // Subscription
  subscription: any;
  
  // Reference to the plupload instance.
  uploader: any;

  // Files being uploaded.
  fileList: any[] = [];

  // Flag to display the uploader only once the library is ready.
  isPluploadReady = false;

  @Input() beginUpload: Subject<void>;

  @Input() referenceId: number;

  @ViewChild('pickfiles', {static: false}) pickfiles: ElementRef;
  
  ngOnInit() {
    this.subscription = this.addPlupload();
    this.beginUpload.subscribe(() => this.uploadFiles());
  }
  
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  addPlupload() {
    return this.addPluploadScript()
      .subscribe(() => {
        this.isPluploadReady = true;
        this.initPlupload();
      });
  }

  // Add a <script> tag to index.html to load Plupload from a CDN.
  addPluploadScript(): Observable<any> {
    const id = 'plupload-sdk';
    // Return immediately if the script tag is already here.
    if (document.getElementById(id)) { return Observable.of(true) }
    let js, fjs = document.getElementsByTagName('script')[0];
    js = document.createElement('script'); js.id = id;
    js.src = "//unpkg.com/plupload@2.3.2/js/plupload.full.min.js";
    fjs.parentNode.insertBefore(js, fjs);
    return Observable.timer(1000).take(1);  // @TODO: Replace this with more robust code
  }

  // Configure and initialize Plupload.
  initPlupload() {

    this.uploader = new plupload.Uploader({
      runtimes : 'html5,html4',
      browse_button : this.pickfiles.nativeElement,
      url : 'http://localhost:52208/api/uploader/',
      filters : {
        max_file_size : '10mb',
        mime_types: [
          {title : "Image files", extensions : "jpg,gif,png"}
        ]
      },
      chunk_size: '100kb',
      multipart_params: { file: {refId: this.referenceId } },
      init: {
        PostInit: () => {
          this.fileList = [];
        },

        FilesAdded: (up, files) => {
          plupload.each(files, (file) => {
            // this.fileList.push(new FileData(
            //   file.id,
            //   file.name,
            //   plupload.formatSize(file.size),
            //   0)
            // );
            this.fileList.push({
              id: file.id,
              name: file.name,
              size: plupload.formatSize(file.size),
              percent: 0
            });
          });
        },

        // Update the upload progress in the list of files displayed in the template.
        UploadProgress: (up, file) => {
          const index = this.fileList.findIndex(f => f.id == file.id);
          this.fileList[index].percent = file.percent;
        },

        Error: (up, err) => {
          console.error(err);
        }
      }
    });

    this.uploader.init();
  }

  uploadFiles() {
    this.uploader.start();
  }

  delete(file: any){
    this.uploader.removeFile(file);
  }
}

export class FileData{
  constructor(
    public id?: number,
    public name?: string,
    public size?: number,
    public percent?: number
  ){}
}