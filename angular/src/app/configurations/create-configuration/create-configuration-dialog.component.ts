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
  ConfigurationDto,
  CreateConfigurationDto,
  ConfigurationServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { forEach as _forEach, map as _map } from "lodash-es";

@Component({
  templateUrl: "create-configuration-dialog.component.html",
})
export class CreateConfigurationDialogComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  configuration = new CreateConfigurationDto();
  permissions: PermissionDto[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _configurationService: ConfigurationServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;

    const configuration = new CreateConfigurationDto();
    configuration.init(this.configuration);

    // configuration.userId = this.appSession.userId;

    this._configurationService.create(configuration).subscribe(
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
