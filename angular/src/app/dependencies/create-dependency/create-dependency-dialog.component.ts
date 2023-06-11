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
  Dependency,
  DependencyServiceProxy,
  CreateDependencyDto,
} from "@shared/service-proxies/service-proxies";
import { forEach as _forEach, map as _map } from "lodash-es";

@Component({
  templateUrl: "create-dependency-dialog.component.html",
  providers: [DependencyServiceProxy],
})
export class CreateDependencyDialogComponent
  extends AppComponentBase
  implements OnInit
{
  saving = false;
  dependency = new CreateDependencyDto();
  permissions: PermissionDto[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _dependencyService: DependencyServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {}

  save(): void {
    this.saving = true;

    const configuration = new CreateConfigurationDto();
    configuration.init(this.dependency);

    this._dependencyService.create(this.dependency).subscribe(
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
