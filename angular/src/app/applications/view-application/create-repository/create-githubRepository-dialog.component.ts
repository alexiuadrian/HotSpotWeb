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
    CreateGithubRepositoryWithApplicationDto,
    Application,
    ApplicationDto,
    GithubRepository,
    ApplicationServiceProxy
  } from "@shared/service-proxies/service-proxies";
  import { forEach as _forEach, map as _map } from "lodash-es";
  
  @Component({
    templateUrl: "create-githubRepository-dialog.component.html",
  })
  export class CreateGithubRepositoryDialogComponent
    extends AppComponentBase
    implements OnInit
  {
    saving = false;
    githubRepository = new CreateGithubRepositoryWithApplicationDto();
    permissions: PermissionDto[] = [];
    checkedPermissionsMap: { [key: string]: boolean } = {};
    defaultPermissionCheckedStatus = true;
    application: ApplicationDto = new ApplicationDto();
  
    @Output() onSave = new EventEmitter<any>();
  
    constructor(
      injector: Injector,
      private _applicationService: ApplicationServiceProxy,
      public bsModalRef: BsModalRef
    ) {
      super(injector);
    }
  
    ngOnInit(): void {}
  
    save(): void {
      this.saving = true;
  
      const githubRepository = new CreateGithubRepositoryWithApplicationDto();
      githubRepository.init(this.githubRepository);
  
      this._applicationService.createGithubRepository(githubRepository).subscribe(
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
  