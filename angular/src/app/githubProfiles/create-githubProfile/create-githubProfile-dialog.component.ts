import {
    Component,
    Injector,
    OnInit,
    EventEmitter,
    Output,
  } from "@angular/core";
  import { BsModalRef } from "ngx-bootstrap/modal";
  import { AppComponentBase } from "@shared/app-component-base";
  import {
    PermissionDto,
    GithubProfileServiceProxy,
    GithubProfile,
    CreateGithubProfileDto,
  } from "@shared/service-proxies/service-proxies";
  import { forEach as _forEach, map as _map } from "lodash-es";
  
  @Component({
    templateUrl: "create-githubProfile-dialog.component.html",
    providers: [GithubProfileServiceProxy],
  })
  export class CreateGithubProfileDialogComponent
    extends AppComponentBase
    implements OnInit
  {
    saving = false;
    githubProfile = new CreateGithubProfileDto();
    permissions: PermissionDto[] = [];
    checkedPermissionsMap: { [key: string]: boolean } = {};
    defaultPermissionCheckedStatus = true;
  
    @Output() onSave = new EventEmitter<any>();
  
    constructor(
      injector: Injector,
      private _githubProfileService: GithubProfileServiceProxy,
      public bsModalRef: BsModalRef
    ) {
      super(injector);
    }
  
    ngOnInit(): void {}
  
    save(): void {
      this.saving = true;
  
      const dependency = new CreateGithubProfileDto();
      dependency.init(this.githubProfile);
  
      this._githubProfileService.create(dependency).subscribe(
        () => {
          this.notify.info(this.l("SavedSuccessfully"));
          this.bsModalRef.hide();
          this.onSave.emit();
        },
        () => {
          this.saving = false;
        }
      );
    }
  }
  