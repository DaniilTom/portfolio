import { Component, ViewChild, ElementRef, Input, Output, OnInit } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/timer';
import 'rxjs/add/operator/take';
import { Subject } from 'rxjs';
import { State } from '../model/base.model';


declare let plupload: any;


@Component({
  selector: 'plupload-comp',
  templateUrl: './plupload.component.html'
})
export class PluploadComponent implements OnInit {

  // Subscription
  subscription: any;

  // Reference to the plupload instance.
  uploader: any;

  // Files being uploaded.
  fileList: PluploadFile[] = [];

  // Flag to display the uploader only once the library is ready.
  isPluploadReady = false;

  mainFile: PluploadFile;

  referenceId: string = 'aasdagfsdgdffdsbf';

  @Input() beginUpload: Subject<string>;
  @Input() deleteAllFiles: Subject<void>;
  @Input() resetMainImage: Subject<void>;

  @Output() uploadComplete: Subject<PluploadFile[]> = new Subject<PluploadFile[]>();
  @Output() mainImageSet: Subject<void> = new Subject<void>();

  @ViewChild('pickfiles', { static: false }) pickfiles: ElementRef;

  @ViewChild('form', { static: false }) form: ElementRef;

  ngOnInit() {
    this.subscription = this.addPlupload();
    this.beginUpload.subscribe((data) => {
      this.referenceId = data;
      if (this.form.nativeElement.checkValidity()) {
        this.uploader.setOption('multipart_params', { file: { refId: this.referenceId } })
        this.uploadFiles();
      }
      else
        alert("Введите имена файлов");
    });
    if (this.deleteAllFiles != undefined)
      this.deleteAllFiles.subscribe(() => this.fileList.forEach(file => this.uploader.splice(0, this.fileList.length)));
    if (this.resetMainImage != undefined)
      this.resetMainImage.subscribe(() => this.fileList.forEach(file =>{
        if(file.isMain)
          file.isMain = false;
        }));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  addPlupload() {
    return this.addPluploadScript()
      .subscribe(() => {
        this.initPlupload();
        this.isPluploadReady = true;
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

    //if(this.uploader != undefined || this.uploader)

    this.uploader = new plupload.Uploader({
      runtimes: 'html5,html4',
      browse_button: this.pickfiles.nativeElement,
      url: 'http://localhost:52208/api/uploader/',
      filters: {
        max_file_size: '10mb',
        mime_types: [
          { title: "Image files", extensions: "jpg,gif,png" }
        ]
      },
      chunk_size: '100kb',
      multipart_params: { file: { refId: this.referenceId } },
      init: {
        PostInit: () => {
          this.fileList = [];
        },

        FilesAdded: (up, files) => {
          plupload.each(files, (file) => {
            // this.fileList.push({
            //   id: file.id,
            //   name: file.name,
            //   size: plupload.formatSize(file.size),
            //   percent: 0
            // });
            var newFile = new PluploadFile();
            newFile.id = file.id;
            newFile.name = file.name;
            newFile.percent = file.percent;
            newFile.size = file.size;
            newFile.type = file.type;
            newFile.state = State.Add;
            this.fileList.push(newFile);
          });
        },

        FilesRemoved: (up, files) => {
          plupload.each(files, (file) => {
            var index = this.fileList.indexOf(this.fileList.find(f => f.id == file.id));
            this.fileList.splice(index, 1);
          });
        },

        UploadProgress: (up, file) => {
          const index = this.fileList.findIndex(f => f.id == file.id);
          this.fileList[index].percent = file.percent;
        },

        UploadComplete: (up, files) => {
          this.uploadComplete.next(this.fileList);
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

  delete(file: any) {
    this.uploader.removeFile(file);
  }

  markAsMain(file: PluploadFile, checkbox: HTMLInputElement) {
    if (this.mainFile != undefined) 
        this.mainFile.isMain = false;

    file.isMain = true;
    this.mainFile = file;
    checkbox.checked = true;

    this.mainImageSet.next();
  }

  // changeTitle(file: PluploadFile, input: HTMLInputElement){
  //   file.title = input.value;
  // }
}

export class PluploadFile {
  constructor(
    public id?: number,
    public name?: string,
    public type?: string,
    public size?: number,
    public percent?: number,
    public isMain: boolean = false,
    public state?: State,
    public title?: string
  ) { }
}