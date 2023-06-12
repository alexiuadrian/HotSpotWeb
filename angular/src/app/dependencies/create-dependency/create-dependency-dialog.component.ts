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
  CreateConfigurationDto,
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

    const dependency = new CreateDependencyDto();
    dependency.init(this.dependency);

    this._dependencyService.create(dependency).subscribe(
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
