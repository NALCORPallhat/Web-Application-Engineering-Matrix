import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
import { IPhoto } from '../models/i-photo';
// import { FileSelectDirective, FileDropDirective, FileUploader } from '../../../node_modules/ng2-file-upload/ng2-file-upload';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() photos: IPhoto[];
  // uploader: FileUploader;
  baseUrl = environment.apiUrl;

  constructor(private authService: AuthService, private userService: UserService) { }

  ngOnInit() {
  }
  /*
  initializeUploader() {
    this.uploader = new FileUploader({
      url:
      this.baseUrl +
      '/users/' +
      JSON.parse(localStorage.getItem('user')).id +
      '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
  }
  */
}
