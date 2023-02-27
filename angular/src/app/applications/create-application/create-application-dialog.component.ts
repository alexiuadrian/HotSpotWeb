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
  ConfigurationServiceProxy,
  ConfigurationDto,
  Configuration,
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
  configName: string;

  configurations: Configuration[] = [];

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _applicationService: ApplicationServiceProxy,
    private _configurationService: ConfigurationServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._configurationService
      .getList(undefined, undefined, undefined)
      .subscribe((result) => {
        this.configurations = result.map((x) =>
          this.configurationDtoToConfiguration(x)
        );
      });
  }

  configurationDtoToConfiguration(
    configuration: ConfigurationDto
  ): Configuration {
    const config = new Configuration();
    config.name = configuration.name;
    config.description = configuration.description;
    config.language = configuration.language;
    config.framework = configuration.framework;
    config.creationTime = configuration.creationTime;
    config.version = configuration.version;
    config.userId = configuration.userId;
    console.log(config);
    return config;
  }

  save(): void {
    this.saving = true;

    const application = new CreateApplicationInput();
    application.init(this.application);

    application.userId = this.appSession.userId;

    if (application.configurations == null) {
      application.configurations = [];
    }

    application.configurations.push(
      this.configurations.find((x) => x.name === this.configName)
    );

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

  onConfigurationChange(): void {
    console.log(this.configName);
    console.log(this.configurations.find((x) => x.name === this.configName));
  }
}
