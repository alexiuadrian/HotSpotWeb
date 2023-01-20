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
  ApplicationDto,
  RoleDto,
  PermissionDto,
  CreateRoleDto,
  PermissionDtoListResultDto,
  ApplicationServiceProxy,
  CreateApplicationInput,
} from "@shared/service-proxies/service-proxies";
import { forEach as _forEach, map as _map } from "lodash-es";

@Component({
  templateUrl: "create-application-dialog.component.html",
})
export class CreateApplicationDialogComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  application = new ApplicationDto();
  permissions: PermissionDto[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;

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

    const application = new CreateApplicationInput();
    application.init(this.application);

    application.userId = this.appSession.userId;

    this._applicationService.create(application).subscribe(
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
